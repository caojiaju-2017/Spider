#!/usr/bin/env python
# -*- coding: utf-8 -*-

#文件简介    ：
#文件创建时间：14-2-19
#作者        ： 'caojiaju'
import random,os,sys,platform,inspect
import hashlib,base64

import os,pickle,socket
import sys
import platform
import time

class publicFunction(object):
    __sessionIdList = []
    def __init__(self):

        return

    ###################################################
    #函数功能：
    #参数    ：paswrd 未加密密码
    #参数    ：paswrd 未加密密码
    #返回值  ：  md5加密后密码
    ####################################################
    @staticmethod
    def getMd5(paswrd):

        h=hashlib.md5()
        h.update(paswrd)
        value = h.digest()
        return base64.encodestring(value)


    ###################################################
    #函数功能：获取32位系统唯一标识
    #返回值  ：32位UUID
    ####################################################
    @staticmethod
    def fetchUUID():
        import uuid

        return unicode(uuid.uuid1())


    ###################################################
    #函数功能：获取当前工作路径
    #返回值  ：String
    ###################################################
    @staticmethod
    def fetchCurrentPath():
        currentPath = None
        for one in ['utf-8','gbk']:
            try:
                currentPath = unicode(os.getcwd(), one)
                break
            except:
                pass
        #currentPath = unicode(os.getcwd())
        return currentPath

    ###################################################
    #函数功能：随机产生用户ID --- 5位
    #返回值  ：int
    ###################################################
    @staticmethod
    def createSessionId():
        sid = 0
        while True:
            sid = random.randint(10000,1000000)
            if sid in publicFunction.__sessionIdList:
                continue
            else:
                publicFunction.__sessionIdList.append(sid)
                break
        return sid

    ###################################################
    #函数功能：获取平台代码，
    #返回值  ：平台类型字符串
    ###################################################
    @staticmethod
    def getPlatInfo():
        strPlat = platform.platform()
        return  strPlat

    ###################################################
    #函数功能：去除字符串首尾指定字符
    #返回值  ：修改后的字符串
    ####################################################
    @staticmethod
    def trimString(source,kickString):
        if not source :
            return None
        source = str(source)
        source = source.lstrip(kickString)
        source = source.rstrip(kickString)
        return source

    ###################################################
    #函数功能：提取目录字符串的父亲目录字符串
    #返回值  ：修改后的字符串
    ####################################################
    @staticmethod
    def getParentPath(strPath):
        if not strPath:
            return None;

        lsPath = os.path.split(strPath);

        if lsPath[1]:
            return lsPath[0];

        lsPath = os.path.split(lsPath[0]);
        return lsPath[0];

    ###################################################
    #函数功能：获取当前函数名
    #返回值  ：函数名称字符串
    ####################################################
    @staticmethod
    def getCurrentFunctionName():
        return inspect.stack()[1][3]

    ###################################################
    #函数功能：获取当前函数名
    #返回值  ：函数名称字符串
    ####################################################
    @staticmethod
    def getCurrentClassName(classObj):
        return classObj.__class__.__name__

    # ###################################################
    # #函数功能：获取终端系统类型
    # #返回值  ：
    # ####################################################
    # @staticmethod
    # def getServerTerminal():
    #     strPlat = publicFunction.getPlatInfo()
    #     strPlat = strPlat.lower()
    #
    #     if "windows" in strPlat:
    #         return Plat.WINDOWS
    #     if "linux" in strPlat:
    #         return Plat.LINUX
    #     if "android" in strPlat:
    #         return  Plat.ANDROID
    #     if "ios" in strPlat:
    #         return  Plat.IOS
    #     else:
    #         return  Plat.UKNOWN


    ###################################################
    #函数功能：获取系统支持的所有错误信息
    #返回值  ：返回数据库错误信息清单
    ####################################################
    @staticmethod
    def getClassName(tabName):
        #创建文件
        tabName = publicFunction.trimString(tabName,' ')
        tabName = tabName.lower()
        tabNameList = tabName.split("_")
        className = ""

        for index,sec in enumerate(tabNameList):
            className = className + sec[0].upper() + sec[1:]

        return className

    @staticmethod
    def transObjectSave(object,srcAdapHandle, descAdaptHandle):
        return

    @staticmethod
    def excuteSqliteSql(data,sqlCmd):
        try:
            data.GLOBAL_DB_ACCESS_HANDLE.getSqlCommandHandle().Modify(sqlCmd)
            return True
        except BaseException,be:
            print 'table create failed!',be
        return False

    @staticmethod
    def transTabSave(clsName,createTableStr,condithion,sdata, descAdaptHandle):
        tabName = None
        exec( "from " + clsName + " import * \n" + "tabName= " + clsName + ".TABLE_NAME ")

        srcAdapHandle = sdata.GLOBAL_DB_ACCESS_HANDLE


        tabObjList = []
        cmdString = "from " + clsName + " import * \n" + "tabObjList = " + clsName + ".getObjectList(%s)"%('condition=' + '"' + condithion + '"')


        exec(cmdString)

        #装入blob
        for one in tabObjList:
            if one.hasBlobField():
                one.loadBlob()

        #切换数据库
        sdata.GLOBAL_DB_ACCESS_HANDLE = descAdaptHandle

        try:
            publicFunction.excuteSqliteSql(sdata,"drop table %s" % (tabName))
        except:
            pass

        publicFunction.excuteSqliteSql(sdata,createTableStr)

        for one in tabObjList:
            try:
                one.Id = None
                one.save()
            except :
                print 'save failed'

        sdata.GLOBAL_DB_ACCESS_HANDLE = srcAdapHandle
        return tabObjList

    @staticmethod
    def getBiHua(word):
        char_wordtable = {}
        char_wordtable[1]=u"一乙"
        char_wordtable[2]=u"丁七乃乜九了二人亻儿入八冂几凵刀刁力勹匕十厂厶又"
        char_wordtable[3]=u"万丈三上下丌个丫丸久乇么义乞也习乡亍于亏亡亿兀凡刃勺千卫叉口囗土士夕大女子孑孓寸小尢尸山巛川工己已巳巾干幺广廾弋弓才门飞马"
        char_wordtable[4]=u"不与丐丑专中丰丹为之乌书予云互亓五井亢什仁仂仃仄仅仆仇仉今介仍从仑仓允元公六兮内冈冗凤凶分切刈劝办勾勿匀化匹区卅升午卞厄厅历及友双反壬天太夫夭孔少尤尹尺屯巴币幻廿开引心忆戈户手扎支攴攵文斗斤方无日曰月木欠止歹殳毋比毛氏气水火爪父爻爿片牙牛犬王瓦肀艺见计订讣认讥贝车邓长闩队韦风乏"
        char_wordtable[5]=u"且丕世丘丙业丛东丝主乍乎乐仔仕他仗付仙仝仞仟仡代令以仨仪仫们兄兰冉册写冬冯凸凹出击刊刍功加务劢包匆北匝卉半卟占卡卢卮卯厉去发古句另叨叩只叫召叭叮可台叱史右叵叶号司叹叻叼叽囚四圣处外央夯失头奴奶孕宁它宄对尔尕尻尼左巧巨市布帅平幼庀弁弗弘归必忉戊戋扑扒打扔斥旦旧未末本札术正母氐民氕永汀汁汇汉灭犯犰玄玉瓜甘生用甩田由甲申电疋白皮皿目矛矢石示礼禾穴立纠艽艾艿节讦讧讨让讪讫训议讯记轧边辽邗邙邛邝钅闪阡阢饥驭鸟龙"
        char_wordtable[6]=u"丞丢乒乓乔乩买争亘亚交亥亦产仰仲仳仵件价任份仿企伉伊伍伎伏伐休众优伙会伛伞伟传伢伤伥伦伧伪伫佤充兆先光全共关兴再军农冰冱冲决凫凼刎刑划刖列刘则刚创劣动匈匠匡华协印危压厌厍吁吃各吆合吉吊同名后吏吐向吒吓吕吖吗囝回囟因囡团在圩圪圬圭圮圯地圳圹场圾壮夙多夷夸夹夺夼奸她好妁如妃妄妆妇妈字存孙宅宇守安寺寻导尖尘尥尧尽屹屺屿岁岂岌州巡巩帆师年并庄庆延廷异式弛当忏忖忙戌戍戎戏成托扛扣扦执扩扪扫扬收旨早旬旭旮旯曲曳有朱朴朵机朽杀杂权次欢此死毕氖氘氽汆汊汐汔汕汗汛汜汝江池污汤汲灯灰爷牝牟犴犷犸玎玑百祁竹米糸纡红纣纤纥约级纨纩纪纫缶网羊羽老考而耒耳聿肉肋肌臣自至臼舌舛舟艮色芄芊芋芍芎芏芑芒芗芝芨虍虫血行衣西观讲讳讴讵讶讷许讹论讼讽设访诀贞负轨达迁迂迄迅过迈邡邢那邦邪邬钆钇闫闭问闯阪阮阱防阳阴阵阶页饧驮驯驰齐"
        char_wordtable[7]=u"两严串丽乱亨亩伯估伲伴伶伸伺似伽佃但位低住佐佑体何佗佘余佚佛作佝佞佟你佣佥佧克免兑兕兵况冶冷冻初删判刨利别刭助努劫劬劭励劲劳匣医卣卤即却卵县君吝吞吟吠吡吣否吧吨吩含听吭吮启吱吲吴吵吸吹吻吼吾呀呃呆呈告呋呐呒呓呔呕呖呗员呙呛呜囤囫园困囱围囵圻址坂均坊坌坍坎坏坐坑块坚坛坜坝坞坟坠声壳奁奂妊妍妒妓妖妗妙妞妣妤妥妨妩妪妫姊姒孚孛孜孝宋完宏寿尬尾尿局屁层岈岍岐岑岔岖岗岘岙岚岛岜希帏帐庇床庋序庐庑库应弃弄弟张形彤彷役彻忌忍忐忑忒志忘忡忤忧忪快忭忮忱忸忻忾怀怃怄怅怆我戒扭扮扯扰扳扶批扼找技抄抉把抑抒抓投抖抗折抚抛抟抠抡抢护报拒拟攸改攻旰旱时旷更杆杈杉杌李杏材村杓杖杜杞束杠条来杨杩极欤步歼每氙氚求汞汨汩汪汰汴汶汹汽汾沁沂沃沅沆沈沉沌沏沐沔沙沛沟没沣沤沥沦沧沩沪泐泛灵灶灸灼灾灿炀牡牢状犹狁狂狃狄狈玖玛甫甬男甸町疔疖疗皂盯矣矶社祀秀私秃究穷系纬纭纯纰纱纲纳纵纶纷纸纹纺纽纾罕羌肓肖肘肚肛肜肝肟肠良芈芘芙芜芟芡芤芥芦芩芪芫芬芭芮芯芰花芳芴芷芸芹芽芾苁苄苇苈苊苋苌苍苎苏苡苣虬补角言证诂诃评诅识诈诉诊诋诌词诎诏译诒谷豆豕豸贡财赤走足身轩轫辛辰迎运近迓返迕还这进远违连迟邑邮邯邰邱邳邴邵邶邸邹邺邻酉里针钉钊钋钌闰闱闲闳间闵闶闷阻阼阽阿陀陂附际陆陇陈陉韧饨饩饪饫饬饭饮驱驳驴鸠鸡麦龟"
        char_wordtable[8]=u"丧乖乳事些亟享京佩佬佯佰佳佴佶佻佼佾使侃侄侈侉例侍侏侑侔侗供依侠侣侥侦侧侨侩侪侬兔兖其具典冼冽净凭凯函刮到刳制刷券刹刺刻刽刿剀剁剂劾势匦卑卒卓单卖卦卧卷卺厕叁参叔取呢呤呦周呱味呵呶呷呸呻呼命咀咂咄咆咋和咎咏咐咒咔咕咖咙咚咛咝哎囹固国图坡坤坦坨坩坪坫坭坯坳坶坷坻坼垂垃垄垅垆备夜奄奇奈奉奋奔妮妯妲妹妻妾姆始姐姑姓委姗孟孢季孤孥学宓宕宗官宙定宛宜宝实宠审尚居屈屉届岢岣岩岫岬岭岱岳岵岷岸岽岿峁峄巫帑帔帕帖帘帙帚帛帜幸底庖店庙庚府庞废建弥弦弧弩弪录彼往征徂径忝忠念忽忿态怂怊怍怏怔怕怖怙怛怜怡怦性怩怪怫怯怵怿戕或戗戽戾房所承抨披抬抱抵抹抻押抽抿拂拄担拆拇拈拉拊拌拍拎拐拓拔拖拗拘拙拚招拢拣拥拦拧拨择放斧斩於旺昀昂昃昆昊昌明昏易昔昕昙朊朋服杪杭杯杰杲杳杵杷杼松板构枇枉枋析枕林枘枚果枝枞枢枣枥枧枨枪枫枭柜欣欧武歧殁殴氓氛沓沫沭沮沱沲河沸油治沼沽沾沿泄泅泊泌泓泔法泖泗泞泠泡波泣泥注泪泫泮泯泱泳泷泸泺泻泼泽泾浅炅炉炊炎炒炔炕炖炙炜炝炬爬爸版牦牧物狍狎狐狒狗狙狞玟玢玩玫玮环现瓮瓯甙画甾畀畅疙疚疝疟疠疡的盂盱盲直知矸矽矾矿砀码祆祈祉秆秉穸穹空竺籴线绀绁绂练组绅细织终绉绊绋绌绍绎经绐罔罗者耵耶肃股肢肤肥肩肪肫肭肮肯肱育肴肷肺肼肽肾肿胀胁臾舍艰苑苒苓苔苕苗苘苛苜苞苟苠苤若苦苫苯英苴苷苹苻茁茂范茄茅茆茇茉茌茎茏茑茔茕茚虎虏虮虱表衩衫衬规觅视诓诔试诖诗诘诙诚诛诜话诞诟诠诡询诣诤该详诧诨诩责贤败账货质贩贪贫贬购贮贯转轭轮软轰迢迤迥迦迨迩迪迫迭迮述迳邾郁郄郅郇郊郎郏郐郑郓采金钍钎钏钐钒钓钔钕钗闸闹阜陋陌降限陔陕隶隹雨青非顶顷饯饰饱饲饴驵驶驷驸驹驺驻驼驽驾驿骀鱼鸢鸣黾齿"
        char_wordtable[9]=u"临举亭亮亲侮侯侵便促俄俅俊俎俏俐俑俗俘俚俜保俞俟信俣俦俨俩俪俭修兹养冒冠剃削剌前剐剑勃勇勉勋匍南卸厘厚受变叙叛呲咣咤咦咧咨咩咪咫咬咭咯咱咳咴咸咻咽咿哀品哂哄哆哇哈哉哌响哏哐哑哒哓哔哕哗哙哚哜哝哞哟哪囿型垌垒垓垛垠垡垢垣垤垦垧垩垫垭垮垲垴城埏复奎奏契奕奖姘姚姜姝姣姥姨姹姻姿威娃娄娅娆娇娈娜孩孪客宣室宥宦宪宫封将尜尝屋屎屏峋峒峙峡峤峥峦差巷帝带帧帮幽庠庥度庭弈弭弯彖彦彪待徇很徉徊律後怎怒思怠急怨总怼恂恃恍恒恢恤恨恪恫恬恸恹恺恻恼恽战扁扃拜括拭拮拯拱拴拶拷拼拽拾持挂指按挎挑挖挝挞挟挠挡挢挣挤挥挪挺政故斫施既昝星映春昧昨昭是昱昴昵昶昼显曷朐枯枰枳枵架枷枸柁柃柄柏某柑柒染柔柘柙柚柝柞柠柢查柩柬柯柰柱柳柽柿栀栅标栈栉栊栋栌栎栏树歪殂殃殄殆殇残段毒毖毗毡氟氡氢泉泵泶洁洄洇洋洌洎洒洗洙洚洛洞津洧洪洫洮洱洲洳洵洹活洼洽派浃浇浈浊测浍济浏浑浒浓浔涎炫炭炮炯炱炳炷炸点炻炼炽烀烁烂烃爰牮牯牲牵狠狡狨狩独狭狮狯狰狱狲玲玳玷玻珀珂珈珉珊珍珏珐珑瓴甚甭畈畋界畎畏疣疤疥疫疬疮疯癸皆皇皈盅盆盈相盹盼盾省眄眇眈眉看眍眨矜矧矩砂砉砌砍砑砒研砖砗砘砚砜砭祓祖祗祚祛祜祝神祠祢禹禺秋种科秒秕秭穿窀突窃窆竖竽竿笃笈类籼籽绑绒结绔绕绗绘给绚绛络绝绞统缸罘罚美羿耍耐耔耷胂胃胄胆背胍胎胖胗胙胚胛胜胝胞胡胤胥胧胨胩胪胫脉舁舡舢舣茈茗茛茜茧茨茫茬茭茯茱茳茴茵茶茸茹茺茼荀荃荆荇草荏荐荑荒荔荚荛荜荞荟荠荡荣荤荥荦荧荨荩荪荫荬荭荮药莒莛虐虹虺虻虼虽虾虿蚀蚁蚂蚤衍衲衽衿袂袄袅要觇览觉訇诫诬语诮误诰诱诲诳说诵诶贰贱贲贳贴贵贶贷贸费贺贻赳赴赵趴轱轲轳轴轵轶轷轸轹轺轻迷迸迹追退送适逃逄逅逆选逊郗郛郜郝郡郢郦郧酊酋重钙钚钛钜钝钞钟钠钡钢钣钤钥钦钧钨钩钪钫钬钭钮钯闺闻闼闽闾阀阁阂陛陟陡院除陧陨险面革韭音顸项顺须飑飒食饵饶饷饺饼首香骁骂骄骅骆骇骈骨鬼鸥鸦鸨鸩"
        char_wordtable[10]=u"乘亳俯俱俳俸俺俾倌倍倏倒倔倘候倚倜借倡倥倦倨倩倪倬倭倮债值倾偌健党兼冢冤冥凄准凇凉凋凌剔剖剜剞剡剥剧勐匪匿卿厝原叟哥哦哧哨哩哭哮哲哳哺哼哽哿唁唆唇唉唏唐唑唔唛唠唢唣唤唧啊圃圄圆垸埂埃埋埒埔埕埘埙埚壶夏套奘奚姬娉娌娑娓娘娟娠娣娥娩娱娲娴婀孬宰害宴宵家宸容宽宾射屐屑展屙峨峪峭峰峻崂崃席帱座弱徐徒徕恁恋恐恕恙恚恝恣恧恩恭息恳恶悃悄悌悍悒悔悖悚悛悝悟悦悭悯扇拳拿挈挚挛挨挫振挹挽捂捃捅捆捉捋捌捍捎捏捐捕捞损捡换捣效敉敌敖斋料旁旃旄旅旆晁晃晋晌晏晒晓晔晕晖晚晟朔朕朗柴栓栖栗栝校栩株栲栳样核根格栽栾桀桁桂桃桄桅框案桉桊桌桎桐桑桓桔桕桠桡桢档桤桥桦桧桨桩梃梆梢梧梨殉殊殷毙毪氤氦氧氨氩泰流浆浙浚浜浞浠浣浦浩浪浮浯浴海浸浼涂涅消涉涌涑涓涔涕涛涝涞涟涠涡涣涤润涧涨涩烈烊烘烙烛烟烤烦烧烨烩烫烬热爱爹特牺狳狴狷狸狺狻狼猁猃玺珙珞珠珥珧珩班珲琊瓞瓶瓷畔留畚畛畜疰疱疲疳疴疸疹疼疽疾痂痃痄病症痈痉皋皱益盍盎盏盐监眙眚真眠眢眩砝弢砟砣砥砧砩砬砰破砷砸砹砺砻砼砾础祟祥祧祯离秘租秣秤秦秧秩秫积称窄窈窍站竞笄笆笊笋笏笑笔笕笫粉粑紊素索紧绠绡绢绣绥绦继绨缺罟罡罢羔羞翁翅耄耆耕耖耗耘耙耸耻耽耿聂胭胯胰胱胲胳胴胶胸胺胼能脂脆脊脍脎脏脐脑脒脓臬臭致舀舐舨航舫般舭舯舰舱艳荷荸荻荼荽莅莆莉莎莓莘莜莞莠莨莩莪莫莰莱莲莳莴莶获莸莹莺莼莽虑虔蚊蚋蚌蚍蚓蚕蚜蚝蚣蚧蚨蚩蚪蚬衄衮衰衷衾袁袍袒袖袜袢被觊请诸诹诺读诼诽课诿谀谁谂调谄谅谆谇谈谊豇豹豺贼贽贾贿赀赁赂赃资赅赆赶起趵趸趿躬軎轼载轾轿辁辂较辱逋逍透逐逑递途逖逗通逛逝逞速造逡逢逦邕部郫郭郯郴郸都酌配酎酏酐酒釜钰钱钲钳钴钵钶钷钸钹钺钻钼钽钾钿铀铁铂铃铄铅铆铈铉铊铋铌铍铎阃阄阅阆陪陬陲陴陵陶陷隼隽难顼顽顾顿颀颁颂颃预饽饿馀馁骊骋验骏高髟鬯鬲鸪鸫鸬鸭鸯鸱鸲鸳鸵"
        char_wordtable[11]=u"龛鸶龀乾偃假偈偎偏偕做停偬偶偷偻偾偿傀兜兽冕减凑凰剪副勒勖勘匏匐匙匮匾厢厣厩唪唬售唯唰唱唳唷唼唾唿啁啃啄商啉啐啕啖啜啡啤啥啦啧啪啬啭啮啵啶啷啸喏喵圈圉圊埝域埠埤埭埯埴埸培基埽堀堂堆堇堋堍堑堕堵够奢娶娼婆婉婊婕婚婢婧婪婴婵婶孰宿寂寄寅密寇尉屠崆崇崎崔崖崛崞崤崦崧崩崭崮巢帷常帻帼庳庵庶康庸庹庾廊弹彗彩彬得徘徙徜恿悉悠患您悫悬悱悴悸悻悼情惆惊惋惕惘惚惜惝惟惦惧惨惬惭惮惯戚戛扈挲捧捩捭据捱捶捷捺捻掀掂掇授掉掊掎掏掐排掖掘掠探接控推掩措掬掭掮掳掴掷掸掺掼揶敏救敕教敛敝敢斛斜断旋旌旎族晗晡晤晦晨曹曼望桫桴桶桷梁梅梏梓梗梦梭梯械梳梵检棂欲欷殍殒殓毫氪涪涫涮涯液涵涸涿淀淄淅淆淇淋淌淑淖淘淙淝淞淠淡淤淦淫淬淮深淳混淹添清渊渌渍渎渐渑渔渖渗渚渠烯烷烹烽焉焊焐焓焕焖焘爽牾牿犁猊猎猓猕猖猗猛猜猝猞猡猪猫率球琅理琉琏琐瓠甜略畦疵痊痍痒痔痕痖皎皑皲盒盔盖盗盘盛眦眭眯眵眶眷眸眺眼着睁矫砦硅硇硌硎硐硒硕硖硗硭票祭祷祸秸移秽稆窑窒窕竟章笙笛笞笠笤笥符笨笪第笮笱笳笸笺笼笾筇粒粕粗粘粜粝累绩绪绫续绮绯绰绱绲绳维绵绶绷绸绺绻综绽绾绿缀缁缍羚羝羟翊翌翎耜聃聆聊聋职聍胬脖脘脚脞脬脯脱脲脶脸舂舳舴舵舶舷舸船舻艴菀菁菅菇菊菌菏菔菖菘菜菝菟菠菡菥菩菪菰菱菲菸菹菽萁萃萄萆萋萌萍萎萏萑萘萜萝萤营萦萧萨萸著虚蚯蚰蚱蚴蚵蚶蚺蛀蛄蛆蛇蛉蛊蛋蛎蛏衅衔袈袋袤袭袱袷袼裆裉觋觖谋谌谍谎谏谐谑谒谓谔谕谖谗谘谙谚谛谜谝豉豚象赇赈赉赊赦赧趺趼趾跃跄距躯辄辅辆逭逮逯逵逶逸逻郾鄂鄄酗酚酝酞野铐铑铒铕铖铗铘铙铛铜铝铞铟铠争铣铤铥铧铨稔铫铬铭铮铯铰铱铲铳铴铵银铷阈阉阊阋阌阍阎阏阐隅隆隈隋隍随隐隗雀雩雪颅领颇颈馄馅馆馗骐骑骒骓骖鸷鸸鸹鸺鸽鸾鸿鹿麸麻黄龚"
        char_wordtable[12]=u"亵傅傈傍傣傥傧储傩傲凿剩割募博厥厦厨啻啼啾喀喁喂喃善喇喈喉喊喋喑喔喘喙喜喝喟喧喱喳喷喹喻喽喾嗖嗟堙堞堠堡堤堪堰塄塔壹奠奥婷婺婿媒媚媛媪嫂孱孳富寐寒寓尊就属屡崴崽崾嵇嵋嵌嵘嵛嵝嵫嵬嵯巯巽帽幂幄幅弑强弼彘彭御徨循悲惑惠惩惫惰惴惶惹惺愀愉愎愕愠愣愤愦愧慌慨戟戡戢扉掌掣掰掾揄揆揉揍揎描提插揖揞揠握揣揩揪揭揲援揸揽揿搀搁搂搅搓搔搜搭搽摒敞散敦敬斌斐斑斯普景晰晴晶晷智晾暂暑曾替最朝期棉棋棍棒棕棘棚棠棣森棰棱棵棹棺棼椁椅椋植椎椐椒椟椠椤椭椰楗楮榔欹欺款殖殚殛毯毳毵毽氮氯氰淼渝渡渣渤渥温渫渭港渲渴游渺湃湄湍湎湓湔湖湘湛湟湫湮湾湿溃溅溆溉溲滁滋滑滞焙焚焦焯焰焱然煮牌牍犀犄犊犋犍猢猥猩猬猱猴猸猹猾琚琛琢琥琦琨琪琬琮琰琳琴琵琶琼瑛瓿甥甯番畲畴疏痘痛痞痢痣痤痦痧痨痪痫登皓皖皴睃睇睐睑矬短硝硪硫硬确硷祺禄禅禽宵稃程稍税窖窗窘窜窝竣童竦筅等筋筌筏筐筑筒答策筘筚筛筝筵粞粟粢粤粥粪紫絮絷缂缃缄缅缆缇缈缉缋缌缎缏缑缒缓缔缕编缗缘缙羡翔翕翘耋耠聒联脔脾腆腈腊腋腌腑腓腔腕腙腚腱腴舄舒舜舾艇萱萼落葆葑葙葚葛葜葡董葩葫葬葭葱葳葵葶葸葺蒂蒇蒈蒉蒋蒌蒎蛐蛑蛔蛘蛙蛛蛞蛟蛤蛩蛭蛮蛰蛱蛲蛳蛴蜒蜓街裁裂装裎裒裕裙裢裣裤裥覃觌觚觞詈谟谠谡谢谣谤谥谦谧貂赋赌赍赎赏赐赓赔赕趁趄超越趋跆跋跌跎跏跑跖跗跚跛跞践辇辈辉辊辋辍辎辜逼逾遁遂遄遇遍遏遐遑遒道遗酡酢酣酤酥釉释量铸铹铺铼铽链铿销锁锂锃锄锅锆锇锈锉锊锋锌锍锎锏锐锑锒锓锔锕阑阒阔阕隔隘隙雁雄雅集雇雯雳靓韩颉颊颌颍颏飓飧飨馇馈馊馋骗骘骚骛鱿鲁鲂鹁鹂鹃鹄鹅鹆鹇鹈黍黑黹鼋鼎"
        char_wordtable[13]=u"催傺傻像剽剿勤叠嗄嗅嗉嗌嗍嗑嗒嗓嗔嗜嗝嗡嗣嗤嗥嗦嗨嗪嗫嗬嗯嗲嗳嗵嗷嘟塌塍塑塘塞塥填塬墓媲媳媵媸媾嫁嫉嫌嫒嫔嫫寝寞尴嵊嵩嵴幌幕廉廒廓彀徭微想愁愆愈愍意愚感愫慈慊慎慑戤戥搋搌搏搐搛搞搠搡搦搪搬携摁摄摅摆摇摈摊摸敫数斟新旒暄暇暌暖暗椴椹椽椿楂楔楚楝楞楠楣楦楫楱楷楸楹楼榀概榄榆榇榈榉榘槌槎槐歃歆歇歌殿毁毂毹氲溏源溘溜溟溢溥溧溪溯溱溴溶溷溺溻溽滂滇滏滓滔滗滚滟滠满滢滤滥滦滨滩漓漠漭煅煊煌煎煜煞煤煦照煨煲煳煸煺牒犏献猷猿獒瑁瑕瑗瑙瑚瑜瑞瑟瑰甄畸畹痰痱痴痹痼痿瘀瘁瘃瘅瘐皙盟睚睛睡睢督睥睦睨睫睬睹瞄矮硼碇碉碌碍碎碑碓碗碘碚碛碜碰禀禁禊福稔稗稚稞稠稣窟窠窥窦筠筢筮筱筲筷筹筻签简粮粱粲粳缚缛缜缝缟缠缡缢缣缤罨罩罪置署群羧耢聘肄肆腠腥腧腩腭腮腰腹腺腻腼腽腾腿舅艄艉蒗蒙蒜蒡蒯蒲蒴蒸蒹蒺蒽蒿蓁蓄蓉蓊蓍蓐蓑蓓蓖蓝蓟蓠蓣蓥蓦蓬虞蛸蛹蛾蜂蜃蜇蜈蜉蜊蜍蜕蜗蜣衙裔裘裟裨裰裱裸裼裾褂褚觎觜解觥触訾詹誉誊谨谩谪谫谬豢貅貉貊赖趑趔跟跣跤跨跪跫跬路跳跷跸跹跺跻躲辏辐辑输辔辞辟遘遛遢遣遥遨鄙鄞鄢鄣酩酪酬酮酯酰酱鉴锖锗锘错锚锛锝锞锟锡锢锣锤锥锦锨锩锪锫锬锭键锯锰锱阖阗阙障雉雍雎雏零雷雹雾靖靳靴靶韪韫韵颐频颓颔颖飕馍馏馐骜骝骞骟骰骱髡魁魂鲅鲆鲇鲈鲋鲍鲎鲐鹉鹊鹋鹌鹎鹏鹑麂鼓鼠龃龄"
        char_wordtable[14]=u"龅龆僖僚僦僧僬僭僮僳儆兢凳劁劂厮嗽嗾嘀嘁嘈嘉嘌嘎嘏嘘嘛嘞嘣嘤嘧塾墁境墅墉墒墙墚夤夥嫖嫘嫜嫠嫡嫣嫦嫩嫱孵察寡寤寥寨屣嶂幔幛廑廖弊彰愿慕慝慢慵慷截戬搴搿摔摘摞摧摭摹摺撂撄撇撖敲斡旖旗暝暧暨榍榕榛榜榧榨榫榭榱榴榷榻槁槊槔槛槟槠槭模歉殡毓滴滹漂漆漉漏演漕漤漩漪漫漯漱漳漶漾潆潇潋潍潢潴澉煽熄熊熏熔熘熙熬犒獍獐瑭瑶瑷璃甍疑瘊瘌瘕瘗瘘瘙瘟瘥瘦瘩睽睾睿瞀瞅瞍碟碡碣碥碧碱碲碳碴碹磁磋禚稳窨窬窭竭端箅箍箐箔箕算箜箝管箢箦箧箨箩箪箫箬箸粹粼粽精糁綦綮缥缦缧缨缩缪缫罂罱罴翟翠翡翥耥聚肇腐膀膂膈膊膏膑膜臧舆舔舞艋蓰蓼蓿蔌蔑蔓蔗蔚蔟蔡蔫蔷蔸蔹蔺蔻蔼蔽蕖蜀蜘蜚蜜蜞蜡蜢蜥蜩蜮蜱蜴蜷蜻蜾蜿蝇蝈蝉螂裳裴裹褊褐褓褙褛褡褪觏觫誓谭谮谯谰谱谲豪貌赘赙赚赛赫跽踅踉踊踌辕辖辗辣遭遮鄯鄱酲酴酵酶酷酸酹酽酾酿銎銮锲锴锵锶锷锸锹锺锻锼锾锿镀镁镂镄镅阚隧雌雒需霁霆静靼鞅韬韶颗馑馒骠骡骢骶骷髦魃魄魅鲑鲒鲔鲕鲚鲛鲜鲞鲟鹕鹗鹘鹚鹛鹜麽鼐鼻"
        char_wordtable[15]=u"龇龈僵僻儇儋凛劈劐勰嘬嘭嘱嘲嘶嘹嘻嘿噌噍噎噔噗噘噙噜噢噶墀增墟墨墩嬉寮履屦嶙嶝幞幡幢廛影徵德慧慰憋憎憔憧憨憬懂戮摩撅撑撒撕撙撞撤撩撬播撮撰撵撷撸撺擒敷暮暴暹槲槽槿樊樗樘樟横樯樱橄橡橥毅滕潘潜潦潭潮潲潸潺潼澄澈澌澍澎澜澳熟熠熨熳熵牖獗獠瑾璀璁璇璋璎璜畿瘛瘠瘢瘤瘪瘫瘼瞌瞎瞑瞒瞢碾磅磉磊磐磔磕磙稷稹稻稼稽稿窳箭箱箴篁篆篇篌篑篓糅糇糈糊糌糍缬缭缮缯羯羰翦翩耦耧聩聪膘膛膝膣艏艘蔬蕃蕈蕉蕊蕙蕞蕤蕨蕲蕴蕺虢蝌蝎蝓蝗蝙蝠蝣蝤蝥蝮蝰蝴蝶蝻蝼蝽蝾螋褒褥褫褴觐觑觯谳谴谵豌豫赜赭趟趣踏踔踝踞踟踢踣踩踪踬踮踯踺躺辘遴遵醅醇醉醋醌鋈镆镇镉镊镌镍镎镏镐镑镒镓镔霄震霈霉靠靥鞋鞍鞑鞒题颚颛颜额飘餍馓馔骣骸骺骼髫髯魇鲠鲡鲢鲣鲤鲥鲦鲧鲨鲩鲫鹞鹣鹤麾黎"
        char_wordtable[16]=u"齑龉龊儒冀凝劓嘴噤器噩噪噫噬噱噻噼嚆圜墼壁壅嬖嬗嬴寰廨廪徼憝憩憷憾懈懊懒懔撼擀擂擅操擎擐擗擞整斓暾樨樵樽樾橇橐橘橙橛橱橹橼檎檠歙殪氅氆氇潞澡澧澶澹激濂濉濑濒熹燃燎燔燕燠燧犟獬獭璞瓢甏甑瘭瘰瘳瘴瘵瘸瘾瘿癀癃盥瞟瞠瞥瞰磨磬磲磺禧穆穑窿篙篚篝篡篥篦篪篮篱篷糕糖糗糙缰缱缲缳缴罹羲翮翰翱耨耩耪聱膦膨膪膳臻蕹蕻蕾薄薅薇薏薛薜薤薨薪薮薯螃螅螈融螓螗螟螨螭螯蟆蟒衡褰褶赝赞赠踱踵踹踽蹀蹁蹂蹄蹉辙辚辨辩遽避邀邂鄹醍醐醑醒醚醛錾镖镗镘镙镛镜镝镞镟隰雕霍霎霏霓霖靛鞔鞘颞颟颠颡飙飚餐髭髹髻魈魉鲭鲮鲰鲱鲲鲳鲴鲵鲶鲷鲸鲺鲻鹦鹧鹨鹾麇麈黉黔默"
        char_wordtable[17]=u"鼽儡嚅嚎嚏嚓壑壕嬲嬷孺嶷徽懋懑懦戴擘擢擤擦曙朦檀檄檐檑檗檩檬濞濠濡濮濯燥燮爵獯璐璨璩甓疃癌癍皤瞧瞩瞪瞬瞳瞵磴磷礁礅穗篼篾簇簋簌簏簖簧糜糟糠縻繁繇罄罅罾羁翳翼膺膻臀臁臂臃臆臊臌艚薰薷薹藁藉藏藐藓螫螬螳螵螺螽蟀蟊蟋蟑蟓蟥襁襄觳謇豁豳貔貘赡赢蹇蹈蹊蹋蹑蹒辫邃邈醢醣鍪镡镢镣镤镥镦镧镨镩镪镫隳霜霞鞠馘骤髀髁魍魏鲼鲽鳃鳄鳅鳆鳇鳊鳋鹩鹪鹫鹬麋黏黛黜黻鼢鼾龋"
        char_wordtable[18]=u"龌龠冁嚣彝懵戳曛曜檫瀑燹璧癔癖癜癞瞻瞽瞿礓礞簟簦簪糨翻艟藕藜藤藩蟛蟠蟪蟮襟覆謦蹙蹦蹩躇邋醪鎏鏊镬镭镯镰镱雠鞣鞫鞭鞯颢餮馥髂髅鬃鬈鳌鳍鳎鳏鳐鹭鹰"
        char_wordtable[19]=u"黝黟黠鼬嚯孽巅攀攉攒曝瀚瀛瀣爆璺瓣疆癣礤簸簿籀籁缵羸羹艨藻藿蘅蘑蘧蟹蟾蠃蠊蠓蠖襞襦警谶蹬蹭蹯蹰蹲蹴蹶蹼蹿酃醭醮醯鏖镲霪霭靡鞲鞴颤骥髋髌鬏魑鳓鳔鳕鳖鳗鳘鳙鹱麒麓"
        char_wordtable[20]=u"麴黢黼鼗嚷嚼壤孀巍攘曦瀵瀹灌獾瓒矍籍糯纂耀蘖蘩蠕蠛譬躁躅酆醴醵镳霰颥馨骧鬓魔鳜鳝鳞"
        char_wordtable[21]=u"鳟黥黧黩黪鼍鼯夔曩灏爝癫礴禳羼蠡蠢赣躏醺鐾露霸霹颦髓"
        char_wordtable[22]=u"鳢麝黯鼙囊懿氍瓤穰耱蘸蘼躐躔镶"
        char_wordtable[23]=u"霾饔饕髑鬻鹳麟攥攫癯罐趱躜颧"
        char_wordtable[24]=u"鬟鼷鼹齄灞矗蠲蠹衢襻躞鑫"
        char_wordtable[25]=u"鬣馕囔戆攮纛"
        char_wordtable[26]=u"蠼爨"

        for index ,one in enumerate(char_wordtable.keys()):
            values = char_wordtable.values()[index]

            if word in values:
                return index + 1


    @staticmethod
    def getProductCodeRange(sid,pcode):
        if sid == "SUBJECT_0001":
            return [pcode + "01",pcode + "04"]
        elif sid == "SUBJECT_0002":
            return [pcode + "02",pcode + "05"]
        elif sid == "SUBJECT_0003":
            return [pcode + "03",pcode + "06"]

        return []

    @staticmethod
    def getSubPCode(sbuid , pcode):
        if sbuid == "SUBJECT_0001":
            return pcode + "01",pcode + "04"
        elif sbuid == "SUBJECT_0002":
            return pcode + "02",pcode + "05"
        elif sbuid == "SUBJECT_0003":
            return pcode + "03",pcode + "06"

        return []

    @staticmethod
    def getCurrentTime(showFlag = True):
        if showFlag:
            return time.strftime('%Y-%m-%d %H:%M:%S',time.localtime(time.time()))
        else:
            return time.strftime('%Y%m%d%H%M%S',time.localtime(time.time()))

    @staticmethod
    def getCurrentDate():
        return time.strftime('%Y-%m-%d',time.localtime(time.time()))


    @staticmethod
    def saveBlob(Blob,Name ,ext, path):
        createFile = os.path.join(path,Name + ext)

        index = 1
        while True:
            if not os.path.isfile(createFile):
                break

            Name = Name + unicode(index)
            createFile = os.path.join(path,Name+ ext)

            index = index + 1

        binStr = base64.b64decode(Blob)

        fileHandle = open(createFile,"wb")
        fileHandle.write(binStr)
        fileHandle.close()
        return Name

    @staticmethod
    def has_keys(dic, *keys):

        for k in keys:

            if k not in dic.keys():

                return False

        return True

    @staticmethod
    def replaceSpacialString(src):
        if src == None:
            return None

        src = src.replace("&hellip;","...")
        src = src.replace("&amp;","&")
        src = src.replace("&lt;","<")
        src = src.replace("&gt;",">")
        src = src.replace("&quot;",'"')
        src = src.replace("&apos;","'")
        src = src.replace("&nbsp;"," ")
        src = src.replace("&nbsp;"," ")

        #处理尖括号
        while True:
            pos1 = src.find("<")
            pos2 = src.find(">")

            #如果只有一个，则替换
            if (pos1 < 0 and pos2 >= 0) or (pos1 >= 0 and pos2 < 0) :
                src = src.replace("<"," ")
                src = src.replace(">"," ")
                break
            elif pos1 < 0 and pos2 < 0:
                break
            src = src[:pos1] + src[pos2 + 1 :]
        return  src

class typeStruct(object):
    def __init__(self):
        self.absPath = None
        self.typeId = None
        self.memCount = 0
        self.typeName = None
        self.typeInfo = None
        self.imageUrl = None
        self.playUrl = None
        self.type = 0  #0 : dir  1:file

class CommitData(object):
    def __init__(self,dbhandle,type):
        self.dbHandle = dbhandle
        self.operatorType = type  # save  delete

def main():
    # publicFunction.sys_version()
    # publicFunction.cpu_mem()
    # publicFunction.disk()
    # publicFunction.network()
    # publicFunction.cpu_use()
    return
if __name__ == '__main__':
    main()
    print platform.system()
    print platform.release()
    print platform.version()
    print platform.platform()
    print platform.machine()