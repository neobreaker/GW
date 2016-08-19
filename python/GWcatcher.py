import re
import urllib
import urllib2
#import BeautifulSoup

DEBUG = 0

class Spider:

    def __init__(self):
        self.siteURL = 'http://www.bnade.com/itemQuery.html?itemName=%E6%97%B6%E6%B2%99%E4%B9%8B%E7%93%B6&realm=%E5%85%8B%E5%B0%94%E8%8B%8F%E5%8A%A0%E5%BE%B7'
        self.m_filename = 'bnade.html'

    def getPage(self):
        url = self.siteURL;
        print url
        
        if not DEBUG :
            request = urllib2.Request(url)
            response = urllib2.urlopen(request).read().decode('utf8')
            f = open(self.m_filename, 'w+')
            f.write(response)
            f.close()
        else :
            f = open(self.m_filename, 'r')
            response = f.read()
            f.close()
        return response

s = Spider();
s.getPage();