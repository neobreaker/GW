# coding=utf-8
import json
import GWcatcher
import pymssql
import DatabaseManager

conn_state = 1
try:
   #conn = pymssql.connect(host="localhost", user="sa", password="sa", database="GoblinWOW", charset = "utf8", login_timeout = 5)
   ms = DatabaseManager.DBManager("localhost", "sa", "sa", "GoblinWOW");
except:
    print 'connect database failed'
    conn_state = 0

if conn_state == 1:
	#print 'success'
	
	msql = "SELECT * FROM dbo.ItemInSale";
	mlist = ms.ExecuteQuery(msql);

	for row in mlist:
		print str(row[0]) + "\t" + row[1] + "\t" + str(row[2]);
		item_name = row[1];
		if(row[2] == 0):		#item id == 0
			s = GWcatcher.Spider();
			try:
				response = s.getItemId(item_name);
				data = json.loads(response);
				print data[0]['id'];
				try:
					updatesql = 'update dbo.ItemInSale set item_id=%d where id=%d' % (data[0]['id'], row[0]);
					print updatesql;
					ms.ExecuteNoQuery(updatesql);
				except Exception, e:
					print e;
			except Exception, e:
				print e
			
			

