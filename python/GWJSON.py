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
		#print str(row[0]) + "\t" + row[1]
		item_name = row[1];
		s = GWcatcher.Spider(item_name);
		try:
			response = s.getItemId();
			json = json.loads(response);
			print json[0]['id'];
		except Exception, e:
			print e
		
		try:
			updatesql = 'update dbo.ItemInSale set item_id=%d where id=%d' % (json[0]['id'], row[0]);
			print updatesql;
			ms.ExecuteNoQuery(updatesql);
		except Exception, e:
			print e;

