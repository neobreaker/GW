# coding=utf-8
import re
import urllib
import urllib2
#import BeautifulSoup

DEBUG = 1

class Spider:

    def __init__(self, item_name):
        self.m_itemname = item_name;

    def getItemId(self):
        content = self.m_itemname
        content = content.encode('utf-8')  
        content = urllib2.quote(content)  
        url = 'http://bnade.com/wow/item/name/%s'%content 
        #print url
        
        if not DEBUG :
            request = urllib2.Request(url)
            response = urllib2.urlopen(request).read().decode('utf-8')
            f = open('./cache/'+self.m_itemname, 'w+')
            f.write(response)
            f.close()
        else :
            f = open('./cache/'+self.m_itemname, 'r')
            response = f.read()
            f.close()
        return response

#s = Spider();
#s.getPage();