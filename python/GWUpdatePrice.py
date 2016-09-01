# coding=utf-8
import json
import GWcatcher
import pymssql
import DatabaseManager
import time

conn_state = 1
try:
   #conn = pymssql.connect(host="localhost", user="sa", password="sa", database="GoblinWOW", charset = "utf8", login_timeout = 5)
   ms = DatabaseManager.DBManager("localhost", "sa", "sa", "GoblinWOW");
except:
    print 'connect database failed'
    conn_state = 0

if conn_state == 1:
	#print 'success'
	format = '%Y-%m-%d %H:%M:%S'
	msql = "SELECT * FROM dbo.ItemInSale";
	mlist = ms.ExecuteQuery(msql);

	for row in mlist:
		print str(row[0]) + "\t" + row[1] + "\t" + str(row[2]);
		s = GWcatcher.Spider();
		try:
			response = s.getPriceList(15, row[2]);
			data = json.loads(response);
			recent_time = data[0][2];
			recent_price = data[0][0];
			min_price = data[0][0];
			max_price = min_price;
			avg_price = 0;
			avg_num = 0;
			cnt = 0;
			for cur in data:
				#value = time.localtime(cur[2]/1000);
				#print time.strftime(format, value);
				if(recent_time < cur[2]):
					recent_time = cur[2];
					recent_price = cur[0];
				if(min_price > cur[0]):	
					min_price = cur[0];
				if(max_price < cur[0]):	
					max_price = cur[0];
				avg_price += cur[0];
				avg_num += cur[1];
				cnt = cnt + 1;
			avg_price = avg_price / cnt;
			avg_num = avg_num / cnt;
			avg_price = avg_price / 10000;
			min_price = min_price / 10000;
			max_price = max_price / 10000;
			recent_price = recent_price / 10000;
			print str(min_price) + '\t' + str(max_price) + '\t' + str(avg_price) + '\t' + str(avg_num) + '\t' + str(recent_price) + '\t' + time.strftime(format, time.localtime(recent_time/1000));
			updatesql = 'update dbo.ItemInSale set lowprice=%d, highprice=%d ,avgprice=%d, avgnum=%d, recentprice=%d where id=%d' % (min_price, max_price, avg_price, avg_num, recent_price, row[0]);
			print updatesql;
			ms.ExecuteNoQuery(updatesql);
		except Exception, e:
				print e;
		
