/*!
 *  版权所有(C) 2021 ToolGood(林知君)
 *  GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
using System;
using System.Collections.Generic;
using System.Linq;

namespace ToolGood.WebCommon.Utils
{
    /// <summary>
    /// 黑名单用户名
    /// </summary>
    public static class UsernameBlacklistUtil
    {
        private static List<string> _names = new List<string>() {
                ".htaccess",".htpasswd",".well-known","_domainkey","400","401","403","404","405","406","407","408","409","410","411","412","413","414",
            "415","416","417","421","422","423","424","426","428","429","431","500","501","502","503","504","505","506","507","508","509","510","511",
            "about","aboutus","about-us","abuse","access","account","accounts","acme","ad","add","admanager","admin","admindashboard","administration",
            "administrator","ads","ads.txt","adsense","adult","advertise","advertising","adword","adwords","aes128-ctr","aes128-gcm","aes192-ctr",
            "aes256-ctr","aes256-gcm","affiliate","affiliatepage","affiliates","afp","ajax","alert","alerts","alpha","amp","analytics","android","answer",
            "answers","ap","api","api1","api2","api3","apis","app","app-ads.txt","appengine","application","appnews","apps","asc","asdf","asset",
            "assets","assets1","assets2","assets3","assets4","assets5","atom","auth","authentication","authorize","autoconfig","autodiscover",
            "auto-discovery","avatar","backup","bank","banner","banners","base","bbs","beginners","beta","billing","billings","binaries","binary",
            "biz","blackberry","blog","blogs","blogsearch","board","book","bookmark","bookmarks","books","broadcasthost","business","buy","buzz","cache",
            "calendar","campaign","captcha","careers","cart","cas","catalog","catalogs","categories","category","cdn","cgi","cgi-bin","chacha20-poly1305",
            "change","channel","channels","chart","charts","chat","checkout","ci","clear","client","clients","clients1","close","cloud","cms","cname",
            "cnarne","code","com","comment","comments","community","compare","compose","config","confirm","confirmation","connect","contact","contacts",
            "contactus","contact-us","content","contest","controlpanel","cookies","copy","copyright","core","corp","count","countries","country","cp",
            "cpanel","create","crossdomain.xml","css","css1","css2","css3","curve25519-sha256","customer","customers","customize","cvs","dashboard","data",
            "db","deals","debug","delete","demo","deploy","deployment","desc","desktop","destroy","dev","devel","developer","developers","development",
            "diffie-hellman-group14-sha1","diffie-hellman-group-exchange-sha256","dir","directory","disconnect","discuss","discussion","dl","dns","dns0",
            "dns1","dns2","dns3","dns4","doc","docs","documentation","documents","domain","domainadmin","domainadministrator","donate","download",
            "downloads","downvote","draft","drop","e","earth","ecdh-sha2-nistp256","ecdh-sha2-nistp384","ecdh-sha2-nistp521","edit","editor","email",
            "enable","encrypted","engine","enterprise","error","errorlog","errors","event","events","example","exception","exit","explore","export",
            "extensions","FALSE","family","faq","faqs","favicon.ico","features","feed","feedback","feedburner","feedproxy","feeds","file","files",
            "filter","finance","folder","folders","follow","follower","followers","following","fonts","forgot","forgotpassword","forgot-password",
            "form","forms","forum","forums","friend","friends","ftp","fuck","fun","fusion","gadget","gears","geographic","get","gettingstarted","git",
            "gmail","go","goto","gov","graph","graphql","graphs","group","groups","guest","guests","guidelines","guides","head","header","help","hide",
            "hmac-sha","hmac-sha1","hmac-sha1-etm","hmac-sha2-256","hmac-sha2-256-etm","hmac-sha2-512","hmac-sha2-512-etm","home","host","hosting",
            "hostmaster","html","htpasswd","htrnl","http","httpd","https","humans.txt","i","icons","image","images","imap","img","img1","img2","img3",
            "import","index","info","insert","investor","investors","invitations","invite","invites","invoice","invoices","ios","ipad","iphone","irnages",
            "irng","is","isatap","issues","it","items","job","jobs","join","js","js'","js1","js2","json","keybase.txt","lab","labs","learn","legal",
            "license","licensing","like","limit","list","lists","live","load","local","localdomain","locale","localhost","location","lock","log","login",
            "logout","logs","lost-password","m","mail","mail0","mail1","mail2","mail3","mail4","mail5","mail6","mail7","mail8","mail9","mailerdaemon",
            "mailer-daemon","manage","manager","map","maps","marketing","marketplace","master","me","media","member","members","message","messages",
            "metrics","mis","misc","mms","mobile","model","models","moderator","modify","money","more","movie","movies","mx","mx1","my","mystore","net",
            "network","networks","new","news","newsite","newsletter","newsletters","next","nil","nobody","noc","none","noreply","no-reply","notification",
            "notifications","ns","ns0","ns1","ns2","ns3","ns4","ns5","ns6","ns7","ns8","ns9","null","oauth","oauth2","offer","offers","online","openid",
            "order","orders","org","other","overview","owa","owner","p0rn","pack","page","pages","partner","partnerpage","partners","passwd","password",
            "pay","payment","payments","people","person","photo","photos","pixel","places","plans","plugins","podcasts","policies","policy","pop","pop3",
            "popular","porn","portal","portfolio","post","postfix","postmaster","poweruser","pr0n","preferences","premium","press","previous","pricing",
            "print","privacy","privacy-policy","private","prod","product","production","products","profile","profiles","project","projects","promo",
            "promotions","proxies","proxy","public","purchase","put","queries","quota","radio","random","reader","recover","redirect","reduce","refund",
            "refunds","register","registration","release","remove","replies","reply","report","reports","request","request-password","research","reset",
            "reset-password","resolve","resolver","response","return","returns","review","reviews","rnail","rnicrosoft","robots.txt","root","rootuser",
            "rsa-sha2-2","rsa-sha2-512","rss","rules","s","sale","sales","sandbox","save","scholar","script","sdk","search","secure","security","select",
            "seminars","server","servers","service","services","session","sessions","settings","setup","share","shift","shop","shopping","shortcut",
            "shortcuts","signin","signout","signup","site","sitemap","sitenews","sites","sketchup","sky","slash","slashinvoice","slut","sms","smtp","soap",
            "software","sorry","sort","source","spreadsheet","spreadsheets","sql","src","srntp","ssh","ssh-rsa","ssl","ssladmin","ssladministrator",
            "sslwebmaster","stage","staging","stat","static","statistic","statistics","stats","status","store","style","styles","stylesheet","stylesheets",
            "subdomain","subscribe","sudo","suggest","suggestqueries","super","superuser","support","survey","surveys","surveytool","svn","sync","sys",
            "sysadmin","sysadmins","system","tablet","tag","tags","talk","talkgadget","team","telnet","terms","terms-of-use","test","tester","testers",
            "testimonials","testing","text","theme","themes","today","tool","toolbar","toolbars","tools","topic","topics","tour","trac","training",
            "translate","translation","translations","translator","trending","trends","trial","TRUE","tutorial","tutorials","txt","ul","umac-128",
            "umac-128-etm","umac-64","umac-64-etm","undefined","unfollow","unlike","unsubscribe","update","upgrade","upload","uploads","usenet","user",
            "username","users","uucp","validation","validations","var","verify","video","videos","video-stats","view","voice","void","vote","vpn","w",
            "wave","webdisk","webmail","webmaster","webrnail","website","whm","whois","widget","widgets","wifi","wiki","wpad","write","ww","www","www1",
            "www2","www3","www4","www-data","wwww","xhtml","xhtrnl","xml","xxx","you","yourname","yourusername","zlib","wwwroot",
        };

        /// <summary>
        /// 是否在黑名单内
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsInBlacklist(string name)
        {
            return _names.Contains(name, StringComparer.InvariantCultureIgnoreCase);
        }

    }
}
