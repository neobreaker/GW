# coding=utf-8
import re
import urllib
import urllib2
#import BeautifulSoup

DEBUG = 1

class Spider:

    def getItemId(self,itemname):
        content = itemname;
        content = content.encode('utf-8')  
        content = urllib2.quote(content)  
        url = 'http://bnade.com/wow/item/name/%s'%content 
        #print url
        
        if not DEBUG :
            request = urllib2.Request(url)
            response = urllib2.urlopen(request).read().decode('utf-8')
            f = open('./cache/'+itemname, 'w+')
            f.write(response)
            f.close()
        else :
            f = open('./cache/'+itemname, 'r')
            response = f.read()
            f.close()
        return response

    def getPriceList(self, FWQ_id, item_id):
        url = 'http://www.bnade.com/wow/auction/past/realm/%d/item/%d' % (FWQ_id, item_id);
        if not DEBUG :
            request = urllib2.Request(url)
            response = urllib2.urlopen(request).read().decode('utf-8')
            f = open('./cache/'+str(FWQ_id)+"_"+str(item_id), 'w+')
            f.write(response)
            f.close()
        else :
            f = open('./cache/'+str(FWQ_id)+"_"+str(item_id), 'r')
            response = f.read()
            f.close()
        return response


#s = Spider();
#s.getPage();