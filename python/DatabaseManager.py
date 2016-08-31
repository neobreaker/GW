# coding=utf-8
import pymssql

class DBManager:
	def __init__(self, host, user, pwd, db):
		self.m_host = host
		self.m_user = user
		self.m_pwd = pwd
		self.m_db = db

	def __GetConnect(self):
		if not self.m_db:
			raise(NameError, "Invalid DataBase Name")

		self.m_connect = pymssql.connect(host = self.m_host, user=self.m_user
										, password=self.m_pwd, database=self.m_db, charset = "utf8", login_timeout = 5)

		m_cursor = self.m_connect.cursor()

		if not m_cursor:
			raise(NameError, "Failed To Connect DataBase")
		else:
			return m_cursor;

	def ExecuteQuery(self, sql):
		cursor = self.__GetConnect()
		cursor.execute(sql)
		result_list = cursor.fetchall()
		self.m_connect.close()
		return result_list

	def ExecuteNoQuery(self, sql):
		cursor = self.__GetConnect()
		cursor.execute(sql)
		self.m_connect.commit()
		self.m_connect.close()
'''
def test():
	ms = DatabaseManager("localhost", "sa", "sa", "CM")
	result_list = ms.ExecuteQuery("SELECT * FROM dbo.Renter")
	for (Rname, Rnumber) in result_list:
		print str(Rname) + "\t" + Rnumber
'''