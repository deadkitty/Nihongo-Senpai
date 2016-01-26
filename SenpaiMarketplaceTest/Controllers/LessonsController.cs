using SenpaiMarketplaceTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SenpaiMarketplaceTest.Controllers
{
    public class LessonsController : ApiController
    {
        // GET: api/Lessons
        public List<Lesson> GetLessons()
        {
            List<Lesson> lessons = new List<Lesson>();

            lessons.Add(new Lesson("75|Tobira L1|0|109"));
            lessons.Add(new Lesson("100|Tobira L2|0|73"));
            lessons.Add(new Lesson("101|Tobira L3|0|84"));

            lessons.Add(new Lesson("102|Genki L5 Kanji|3|14"));
            lessons.Add(new Lesson("103|Genki L6 Kanji|3|15"));
            
            lessons.Add(new Lesson("104|Minna no Nihongo Fukushuu J Test|1|3"));

            return lessons;
        }

        // GET: api/Lessons/5
        [ResponseType(typeof(Lesson))]
        public IHttpActionResult GetLesson(int id)
        {
            Lesson lesson = GetLessons().Where(x => x.Id == id).FirstOrDefault();
            
            switch(lesson.Id)
            {
                case 75: FillTobiraL1(lesson); break;
                case 100: FillTobiraL2(lesson); break;
                case 101: FillTobiraL3(lesson); break;
                case 102: FillGenkiL5Kanji(lesson); break;
                case 103: FillGenkiL6Kanji(lesson); break;
                case 104: FillMinnaNoNihongoFukushuuJ(lesson); break;
            }
            
            return Ok(lesson);
        }

        #region Fill Test Lessons

        private void FillTobiraL1(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("20055|ききかえす|聞き返す|eine Frage aufwerfen über etwas das gerade gesagt wurde|に～|1"));
            lesson.Words.Add(new Word("20056|むかしばなし|昔話|alte Geschichte, Märchen||0"));
            lesson.Words.Add(new Word("20057|せんこう|専攻|Fachgebiet, Hauptfach|を～ ... する|0"));
            lesson.Words.Add(new Word("20058|いろいろ|色々|viele verschiedene|～と|6"));
            lesson.Words.Add(new Word("20059|せんもん|専門|Hauptfach||0"));
            lesson.Words.Add(new Word("20060|ちほう|地方|Region, Gebiet, Land||0"));
            lesson.Words.Add(new Word("20061|いなか|田舎|Landschaft, Landgebiet||0"));
            lesson.Words.Add(new Word("20062|そのほか|その他|auserdem, daneben|～に|11"));
            lesson.Words.Add(new Word("20063|かんとう|関東|Kanto (Region)|～（地方）|0"));
            lesson.Words.Add(new Word("20064|かんさい|関西|Kansai (Region)|～（地方）|0"));
            lesson.Words.Add(new Word("20065|まわり|周り|Umgebung, Umfeld, Nähe, Nachbarschaft||0"));
            lesson.Words.Add(new Word("20066|はいる|入る|hereinkommen/gehen, beitreten, eingebunden werden|が～|1"));
            lesson.Words.Add(new Word("20067|ないよう|内容|Inhalt||0"));
            lesson.Words.Add(new Word("20068|ちめい|地名|Ortsname||0"));
            lesson.Words.Add(new Word("20069|めいぶつ|名物|bekanntes Produkt, lokale Spezialität||0"));
            lesson.Words.Add(new Word("20070|かんけいがある|関係がある|im zusammenhang mit ..., eine Verbindung haben mit ...|に/と～|11"));
            lesson.Words.Add(new Word("20071|でんとうてき|伝統的|traditionell||5"));
            lesson.Words.Add(new Word("20072|ぎょうじ|行事|Event||0"));
            lesson.Words.Add(new Word("20073|とし|年|Jahr||0"));
            lesson.Words.Add(new Word("20074|とくべつ|特別|speziell, besonders||5"));
            lesson.Words.Add(new Word("20075|おこなう|行う|betreiben, durchführen|を～|1"));
            lesson.Words.Add(new Word("20076|しょうがつ|正月|Neujahr|お～|0"));
            lesson.Words.Add(new Word("20077|そういう||(dinge) wie dieses, diese Sorte von (geschichten)||8"));
            lesson.Words.Add(new Word("20078|～たち|～達|Pluralmarker für Personen||10"));
            lesson.Words.Add(new Word("20079|そんな||dieses da, wie das|beim Sprecher|8"));
            lesson.Words.Add(new Word("20080|いっぱんてき|一般的|generell, üblich||5"));
            lesson.Words.Add(new Word("20081|かみしばい|紙芝居|Papierbilder-Schaukastentheater|Erzählen von Märchen anhand von Bildern auf der Straße|0"));
            lesson.Words.Add(new Word("20082|え|絵|Bild||0"));
            lesson.Words.Add(new Word("20083|たいへん|大変|sehr, überaus, auserordentlich||6"));
            lesson.Words.Add(new Word("20084|ほんとう|本当|wirklich, wahr, real|の-Ａｄｊ|8"));
            lesson.Words.Add(new Word("20085|しゅっしん|出身|jemandes Ursprung (stadt, land, etc.)||0"));
            lesson.Words.Add(new Word("20086|しぜん|自然|Natur||0"));
            lesson.Words.Add(new Word("20087|きびしい|厳しい|streng, ernst||4"));
            lesson.Words.Add(new Word("20088|きもち|気持ち|Gefühl, Stimmung, Empfinden||0"));
            lesson.Words.Add(new Word("20089|つゆ|梅雨|Regensaison in Japan||0"));
            lesson.Words.Add(new Word("20090|はじめ|初め|der Anfang||0"));
            lesson.Words.Add(new Word("20091|しつど|湿度|Feuchtigkeit||0"));
            lesson.Words.Add(new Word("20092|うまい|旨い|Lecker, Köstlich|meistens von Männern benutzt|4"));
            lesson.Words.Add(new Word("20093|ジャガいも|ジャガ芋|Kartoffel||0"));
            lesson.Words.Add(new Word("20094|とうほくちほう|東北地方|Tohoku Region||0"));
            lesson.Words.Add(new Word("20095|まつり|祭り|Fest, Festival||0"));
            lesson.Words.Add(new Word("20096|すごい|凄い|Erstaunlich, Krass, toll, schrecklich, entsetzlich||4"));
            lesson.Words.Add(new Word("20097|かんこうきゃく|観光客|Tourist||0"));
            lesson.Words.Add(new Word("20098|ぼく|僕|Ich|meist von Männern benutzt|0"));
            lesson.Words.Add(new Word("20099|おすすめ|お勧め|jemandes Empfehlung||0"));
            lesson.Words.Add(new Word("20100|ちり|地理|Geografie||0"));
            lesson.Words.Add(new Word("20101|みなさん|皆さん|jeder, jedermann||0"));
            lesson.Words.Add(new Word("20102|おおき|大き|groß, riesig|nur als Nomen Modifizierer|5"));
            lesson.Words.Add(new Word("20103|しま|島|Insel||0"));
            lesson.Words.Add(new Word("20104|たいりく|大陸|Kontinent||0"));
            lesson.Words.Add(new Word("20105|しまぐに|島国|Inselstaat||0"));
            lesson.Words.Add(new Word("20106|とし|都市|Stadt||0"));
            lesson.Words.Add(new Word("20107|こくど|国土|Territorium, Land||0"));
            lesson.Words.Add(new Word("20108|ほっかいどう|北海道|Hokkaido|Japans nördlichste Insel|0"));
            lesson.Words.Add(new Word("20109|ほんしゅう|本州|Honshuu|Japans größte Insel|0"));
            lesson.Words.Add(new Word("20110|しこく|四国|Shikoku|kleinste Insel der 4 Hauptinseln|0"));
            lesson.Words.Add(new Word("20111|きゅうしゅう|九州|Kyuushuu|Japans 3. Größte Insel|0"));
            lesson.Words.Add(new Word("20112|～いじょう|～以上|mehr als ..., über ...||9"));
            lesson.Words.Add(new Word("20113|ぜんたい|全体|gesamt, ganz, generell||8"));
            lesson.Words.Add(new Word("20114|２５ふんの１|２５分の１|1/25||11"));
            lesson.Words.Add(new Word("20115|と/どう/ふ/けん|都/道/府/県|Präfektur|4 stk.|0"));
            lesson.Words.Add(new Word("20116|しゅと|首都|Hauptstadt||0"));
            lesson.Words.Add(new Word("20117|そのほか|その他|der rest, die anderen||11"));
            lesson.Words.Add(new Word("20118|ふじさん|富士山|Fujiyama||0"));
            lesson.Words.Add(new Word("20119|せんそう|戦争|Krieg||0"));
            lesson.Words.Add(new Word("20120|おそろしい|恐ろしい|furchteregend, erschreckend||4"));
            lesson.Words.Add(new Word("20121|へいわ|平和|Frieden||0"));
            lesson.Words.Add(new Word("20122|つたえる|伝える|mitteilen, erzählen, informieren|～に、～を|2"));
            lesson.Words.Add(new Word("20123|げんばくドーム|原爆ドーム|Atombombenkuppel|広島|0"));
            lesson.Words.Add(new Word("20124|なんぼく|南北|Norden und Süden||0"));
            lesson.Words.Add(new Word("20125|きこう|気候|Klima||0"));
            lesson.Words.Add(new Word("20126|ひ|日|Tag||0"));
            lesson.Words.Add(new Word("20127|きおん|気温|(atmosphärische)Temperatur||0"));
            lesson.Words.Add(new Word("20128|さ|差|Differenz, Unterschied||0"));
            lesson.Words.Add(new Word("20129|せっし|摂氏|Hundertteilig(zb. cm), Celsius||0"));
            lesson.Words.Add(new Word("20130|さくら|桜|Kirschblüten||0"));
            lesson.Words.Add(new Word("20131|おわり|終わり|das Ende||0"));
            lesson.Words.Add(new Word("20132|ひとびと|人々|Leute||0"));
            lesson.Words.Add(new Word("20133|はなみ|花見|Hanami||0"));
            lesson.Words.Add(new Word("20134|たのしむ|楽しむ|genießen||1"));
            lesson.Words.Add(new Word("20135|めいしょ|名所|bekannter/berühmter Ort||0"));
            lesson.Words.Add(new Word("20136|たとえば|例えば|zum Beispiel||6"));
            lesson.Words.Add(new Word("20137|もっとも|最も|meistens, äußerst||6"));
            lesson.Words.Add(new Word("20138|うつくしい|美しい|schön, hübsch||4"));
            lesson.Words.Add(new Word("20139|しろ|城|Burg|お～|0"));
            lesson.Words.Add(new Word("20140|ユネスコ||UNESCO||0"));
            lesson.Words.Add(new Word("20141|せかいいさん|世界遺産|Welterbe, Weltkulturerbe||0"));
            lesson.Words.Add(new Word("20142|～まえ|～前|vor ...|Jahre, Tage, etc.|9"));
            lesson.Words.Add(new Word("20143|かべ|壁|Wand||0"));
            lesson.Words.Add(new Word("20144|のこる|残る|bleiben, übrig bleiben||1"));
            lesson.Words.Add(new Word("20145|たてもの|建物|Gebäude||0"));
            lesson.Words.Add(new Word("20146|かたち|形|Form, Aussehen, Erscheinung||0"));
            lesson.Words.Add(new Word("20147|しらさぎ|白鷺|Silberreiher|Vogel|0"));
            lesson.Words.Add(new Word("20148|はね|羽|Flügel||0"));
            lesson.Words.Add(new Word("20149|ひろげる|広げる|ausbreiten, erweitern, breiter machen||2"));
            lesson.Words.Add(new Word("20150|さつえいする|撮影する|filmen, Dreharbeiten machen||3"));
            lesson.Words.Add(new Word("20151|かざん|火山|Vulkan||0"));
            lesson.Words.Add(new Word("20152|かんこうする|観光する|Sightseeing, besichtigen||3"));
            lesson.Words.Add(new Word("20153|レジャー||Freizeit||0"));
            lesson.Words.Add(new Word("20154|もくてき|目的|Zweck, Grund||0"));
            lesson.Words.Add(new Word("20155|ゆかた|浴衣|Yukata|informeller Baumwollkimono für den Sommer|0"));
            lesson.Words.Add(new Word("20156|リラックスする||relaxen||3"));
            lesson.Words.Add(new Word("20157|けしき|景色|Aussicht, Landschaft, Kulisse||0"));
            lesson.Words.Add(new Word("20158|とくに|特に|speziell, insbesondere, besonders||6"));
            lesson.Words.Add(new Word("20159|し|市|Kleinstadt, Gemeinde||0"));
            lesson.Words.Add(new Word("20160|～かい|～階|Stockwerk||0"));
            lesson.Words.Add(new Word("20161|ま|間|Raum||0"));
            lesson.Words.Add(new Word("20162|けんがくする|見学する|Besichtigen||3"));
            lesson.Words.Add(new Word("20163|みやげばなし|土産話|Reise- Anekdote/Geschichte||0"));
        }

        private void FillTobiraL2(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("20164|あやまる|謝る|sich entschuldigen||1"));
            lesson.Words.Add(new Word("20165|ごめんなさい||Entschuldigung||11"));
            lesson.Words.Add(new Word("20166|かちょう|課長|Sektionsleiter||0"));
            lesson.Words.Add(new Word("20167|～すぎ|～過ぎ|nach~, über~, zu viel~||9"));
            lesson.Words.Add(new Word("20168|むりをする|無理をする|sich überanstrengen, etw. Schwieriges versuchen||3"));
            lesson.Words.Add(new Word("20169|おせわになる|お世話になる|jmd. zur Last fallen/Umstände bereiten||11"));
            lesson.Words.Add(new Word("20170|げんご|言語|Sprache||0"));
            lesson.Words.Add(new Word("20171|ひょうげん|表現|Ausdruck, Formulierung, Darstellung||0"));
            lesson.Words.Add(new Word("20172|りゆう|理由|Grund, Zweck||0"));
            lesson.Words.Add(new Word("20173|じょうきょう|状況|Situation||0"));
            lesson.Words.Add(new Word("20174|ふくさつ|複雑|kompliziert||5"));
            lesson.Words.Add(new Word("20175|じつは|実は|Es ist so das ..., Ehrlich gesagt ...||11"));
            lesson.Words.Add(new Word("20176|きまり|決まり|Festlegung, Entscheidung, Regel, Vorschrift||0"));
            lesson.Words.Add(new Word("20177|あいて|相手|Partner, Kamerad, die Person zu der jmd. spricht||0"));
            lesson.Words.Add(new Word("20178|かえる|変える|ändern, verändern||2"));
            lesson.Words.Add(new Word("20179|ぶぶん|部分|Teil, Abschnitt||0"));
            lesson.Words.Add(new Word("20180|だんせい|男性|männlich||0"));
            lesson.Words.Add(new Word("20181|じょせい|女性|weiblich||0"));
            lesson.Words.Add(new Word("20182|くらべる|比べる|vergleichen||2"));
            lesson.Words.Add(new Word("20183|かんじ|感じ|Gefühl, Empfindung, Eindruck||0"));
            lesson.Words.Add(new Word("20184|おおくの|多くの|viel, zahlreich, die meisten, der größte Teil von ...||8"));
            lesson.Words.Add(new Word("20185|ばめん|場面|Schauplatz, Situation, Szene||0"));
            lesson.Words.Add(new Word("20186|かんさつ|観察|Beobachtung, Beaufsichtigung, Überwachung|～する|0"));
            lesson.Words.Add(new Word("20187|ひょう|表|Tabelle, Diagramm||0"));
            lesson.Words.Add(new Word("20188|だんじょ|男女|Mann und Frau||0"));
            lesson.Words.Add(new Word("20189|おごる|奢る|jmd. einladen/bewirten|zum Essen, Tee, etc.|1"));
            lesson.Words.Add(new Word("20190|もじ|文字|Schriftzeichen, Buchstabe||0"));
            lesson.Words.Add(new Word("20191|こいびと|恋人|Freund/Freundin||0"));
            lesson.Words.Add(new Word("20192|れい|例|Beispiel||0"));
            lesson.Words.Add(new Word("20193|さいご|最後|Ende, Schluss||0"));
            lesson.Words.Add(new Word("20194|れんらく|連絡|Kontakt, Kommunikation|～する|0"));
            lesson.Words.Add(new Word("20195|はっきり||klar, deutlich||6"));
            lesson.Words.Add(new Word("20196|さそう|誘う|einladen, fragen|jmd. zu ihrgentwas ...|1"));
            lesson.Words.Add(new Word("20197|ことわる|断る|ablehnen, absagen, verweigern||1"));
            lesson.Words.Add(new Word("20198|つごうがわるい|都合が悪い|ungünstig sein, nicht passen, ungelegen sein||4"));
            lesson.Words.Add(new Word("20199|きぶん|気分|Stimmung, Verfassung, Laune||0"));
            lesson.Words.Add(new Word("20200|おねがい|お願い|Wunsch, Hoffnung, Bitte|～する|0"));
            lesson.Words.Add(new Word("20201|たいせつにする|大切にする|schätzen, achten, sorgsam mit etw. umgehen||3"));
            lesson.Words.Add(new Word("20202|かんたん|簡単|leicht, einfach||5"));
            lesson.Words.Add(new Word("20203|なれる|慣れる|sich gewöhnen, sich anpassen, vertraut werden||2"));
            lesson.Words.Add(new Word("20204|とくちょう|特徴|Eigentümlichkeit, Besonderheit, charakteristisches Merkmal||0"));
            lesson.Words.Add(new Word("20205|ひつよう|必要|Notwendigkeit, Bedürfniss||0"));
            lesson.Words.Add(new Word("20206|けいたいでんわ|携帯電話|Handy||0"));
            lesson.Words.Add(new Word("20207|ふつう|普通|Normalität, Durchschnittlichkeit||0"));
            lesson.Words.Add(new Word("20208|ろんぶん|論文|Aufsatz, Artikel, Arbeit, Dissetation, ...||0"));
            lesson.Words.Add(new Word("20209|あう|合う|passen, übereinstimmen||1"));
            lesson.Words.Add(new Word("20210|けいご|敬語|Höflicher Ausdruck, Höflichkeitsform||0"));
            lesson.Words.Add(new Word("20211|か|課|Lektion||0"));
            lesson.Words.Add(new Word("20212|ていねい|丁寧|höflich, respektvoll, gründlich, sorgfältig||5"));
            lesson.Words.Add(new Word("20213|くだけた|砕けた|einfach, entspannt, informell||8"));
            lesson.Words.Add(new Word("20214|つかいわける|使い分ける|etw. angemessen benutzen||2"));
            lesson.Words.Add(new Word("20215|ぶんまつ|文末|Satzende||0"));
            lesson.Words.Add(new Word("20216|あらわれる|現れる|erscheinen, auftauchen||2"));
            lesson.Words.Add(new Word("20217|あのかた|あの方|diese Person|höflich|11"));
            lesson.Words.Add(new Word("20218|あいつ|彼奴|der Typ da|vulgär|0"));
            lesson.Words.Add(new Word("20219|おれ|俺|Ich|von Männern benutzt|0"));
            lesson.Words.Add(new Word("20220|このへんに|この辺に|hier in der Gegend||11"));
            lesson.Words.Add(new Word("20221|ずいぶん|随分|viel, ziemlich, sehr||6"));
            lesson.Words.Add(new Word("20222|しょうりゃくする|省略する|auslassen, abkürzen, weglassen||3"));
            lesson.Words.Add(new Word("20223|たんしゅくけい|短縮形|verkürzte Form||0"));
            lesson.Words.Add(new Word("20224|はなしことば|話言葉|gesprochene Sprache||0"));
            lesson.Words.Add(new Word("20225|こういう||von dieser sorte, wie dieses||11"));
            lesson.Words.Add(new Word("20226|とうち|倒置|Umkehrung, Inversion, Wechsel||0"));
            lesson.Words.Add(new Word("20227|このような||so ein ..., solch ein ...||11"));
            lesson.Words.Add(new Word("20228|かきことば|書き言葉|geschriebene Sprache||0"));
            lesson.Words.Add(new Word("20229|～たい|～体|Style|im Sprachgebrauch|9"));
            lesson.Words.Add(new Word("20230|きょうみぶかい|興味深い|sehr interessant||4"));
            lesson.Words.Add(new Word("20231|かきことばてき|書き言葉的|wie geschriebene sprache ...||5"));
            lesson.Words.Add(new Word("20232|～てき|～的|...lich, ...isch, ...mäßig||9"));
            lesson.Words.Add(new Word("20233|おたく|お宅|jemandes Zuhause|höflich|0"));
            lesson.Words.Add(new Word("20234|それでは||dann, wenn das so ist, in dem fall||11"));
            lesson.Words.Add(new Word("20235|しょうしょう|少々|ein wenig, ein bisschen|formell|6"));
            lesson.Words.Add(new Word("20236|きゅう|急|plötzlich, auf einmal||5"));
        }

        private void FillTobiraL3(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("20237|ぎじゅつ|技術|Technologie, Technik, Fähigkeit||0"));
            lesson.Words.Add(new Word("20238|はったつする|発達する|entwickeln, wachsen||3"));
            lesson.Words.Add(new Word("20239|かいじょう|会場|Versammlungsort||0"));
            lesson.Words.Add(new Word("20240|にがおえ|似顔絵|Portrait, Abbild||0"));
            lesson.Words.Add(new Word("20241|かく|描く|malen, zeichnen||1"));
            lesson.Words.Add(new Word("20242|くも、クモ|蜘蛛|Spinne||0"));
            lesson.Words.Add(new Word("20243|てんじょう|天井|Zimmerdecke||0"));
            lesson.Words.Add(new Word("20244|しゅじゅつ|手術|Operation, chirurgischer Eingriff|～をする|0"));
            lesson.Words.Add(new Word("20245|すでに|既に|bereits, vorher||6"));
            lesson.Words.Add(new Word("20246|じっさいに|実際に|tatsächlich, in Wahrheit||11"));
            lesson.Words.Add(new Word("20247|しゃかい|社会|Gesellschaft, Welt, Öffentlichkeit||0"));
            lesson.Words.Add(new Word("20248|かつやくする|活躍する|tätig sein, eine aktive/wichtige Rolle spielen, wirksam sein||3"));
            lesson.Words.Add(new Word("20249|留守番をする|留守番をする|das Haus Hüten|während eine Person nicht da ist|11"));
            lesson.Words.Add(new Word("20250|はこぶ|運ぶ|tragen, transportieren||1"));
            lesson.Words.Add(new Word("20251|にんげん|人間|Mensch||0"));
            lesson.Words.Add(new Word("20252|くらす|暮らす|leben||1"));
            lesson.Words.Add(new Word("20253|とし|年|Jahr, Alter, jahre alt||0"));
            lesson.Words.Add(new Word("20254|ケアハウス||Altenheim||0"));
            lesson.Words.Add(new Word("20255|あざらし、アザラシ|海豹|Seehund||0"));
            lesson.Words.Add(new Word("20256|け|毛|Haar, Fell, Federn, Behaarung||0"));
            lesson.Words.Add(new Word("20257|さわる|触る|berühren, fühlen|etw.|1"));
            lesson.Words.Add(new Word("20258|くび|首|Nacken||0"));
            lesson.Words.Add(new Word("20259|うごかす|動かす|bewegen|etw.|1"));
            lesson.Words.Add(new Word("20260|こえ|声|Stimme||0"));
            lesson.Words.Add(new Word("20261|まわり|周り|Umgebung, Umfeld, Nähe, Nachbarschaft||0"));
            lesson.Words.Add(new Word("20262|あつまる|集まる|sammeln||1"));
            lesson.Words.Add(new Word("20263|どうぶつ|動物|Tier||0"));
            lesson.Words.Add(new Word("20264|アレルギー||Allergie||0"));
            lesson.Words.Add(new Word("20265|だいじょうぶ|大丈夫|sicher, alles in Ordnung||5"));
            lesson.Words.Add(new Word("20266|あっ||ahh!!, ohh!!||11"));
            lesson.Words.Add(new Word("20267|だいじ|大事|wichtig||5"));
            lesson.Words.Add(new Word("20268|こうか|効果|Wirksamkeit, Effektivität, Resultat,Effekt||0"));
            lesson.Words.Add(new Word("20269|ギネスブック||Guinnesbuch der Rekorde||0"));
            lesson.Words.Add(new Word("20270|のる|載る|berichtet werden, erscheinen|in nem Magazin, etc.|1"));
            lesson.Words.Add(new Word("20271|にゅうがくする|入学する|in die Schule Eintretten, immatrikulieren||3"));
            lesson.Words.Add(new Word("20272|ごうかくする|合格する|bestehen|einen Test, etc.|3"));
            lesson.Words.Add(new Word("20273|おいわい|お祝い|Glückwunsch, Gratulation||0"));
            lesson.Words.Add(new Word("20274|はじめて|初めて|zum ersten Mal||6"));
            lesson.Words.Add(new Word("20275|さいしょ|最初|als erstes||6"));
            lesson.Words.Add(new Word("20276|さいしょ|最初|der Anfang||0"));
            lesson.Words.Add(new Word("20277|つける||bezeichnen, benennen|名前を～|2"));
            lesson.Words.Add(new Word("20278|りかいする|理解する|verstehen,  begreifen, erfassen, einsehen||3"));
            lesson.Words.Add(new Word("20279|うごく|動く|sich bewegen, gehen, funktionieren||1"));
            lesson.Words.Add(new Word("20280|ざんねん|残念|bedauerlich, bedauernswert, enttäuschend, ärgerlich||5"));
            lesson.Words.Add(new Word("20281|これから||von jetzt an, in Zukunft||6"));
            lesson.Words.Add(new Word("20282|うまれる|生まれる|geboren werden, zur Welt kommen||2"));
            lesson.Words.Add(new Word("20283|えんそうする|演奏する|spielen, vortragen, darbieten|Musik|3"));
            lesson.Words.Add(new Word("20284|がくしゅうする|学習する|lernen, studieren||3"));
            lesson.Words.Add(new Word("20285|しょくじする|食事する|eine Mahlzeit haben||3"));
            lesson.Words.Add(new Word("20286|さいこう|最高|das Höchste, das Beste, das Maximum, das Größte||0"));
            lesson.Words.Add(new Word("20287|うけつけ|受付|Rezeption, Auskunft, Empfang||0"));
            lesson.Words.Add(new Word("20288|あんないする|案内する|jmd. führen, geleiten, begleiten, benachrichtigen||3"));
            lesson.Words.Add(new Word("20289|げんそく|原則|Prinzip, Grundsatz, Regel||0"));
            lesson.Words.Add(new Word("20290|けがする|怪我する|verletzt werden, sich verletzen||3"));
            lesson.Words.Add(new Word("20291|いはんする|違反する|ein Vergehen begehen, übertreten, verletzten, verstoßen||3"));
            lesson.Words.Add(new Word("20292|まもる|守る|beschützen, verteidigen||1"));
            lesson.Words.Add(new Word("20293|もんだい|問題|Problem, Frage, Fehler||0"));
            lesson.Words.Add(new Word("20294|いらいする|依頼する|beauftragen mit, bitten um, sich verlassen auf||3"));
            lesson.Words.Add(new Word("20295|かんしゃする|感謝する|danken, dankbar sein||3"));
            lesson.Words.Add(new Word("20296|ホームステイさき|ホームステイ先|Gastfamilie||0"));
            lesson.Words.Add(new Word("20297|たのむ|頼む|bitten, um einen Gefallen bitten, bestellen||1"));
            lesson.Words.Add(new Word("20298|～くん|～君|Namenserweiterung für männer von gleichen oder niedrigeren Status||9"));
            lesson.Words.Add(new Word("20299|まちがう|間違う|einen Fehler machen||1"));
            lesson.Words.Add(new Word("20300|ハイブリッド（しゃ）|ハイブリッド（車）|Hybridauto||0"));
            lesson.Words.Add(new Word("20301|エコカー||Umweltfreundliches Auto||0"));
            lesson.Words.Add(new Word("20302|チェックする||etw. checken/überprüfen||3"));
            lesson.Words.Add(new Word("20303|はつおんする|発音する|aussprechen||3"));
            lesson.Words.Add(new Word("20304|もと|元|Wurzel, Ursprung||0"));
            lesson.Words.Add(new Word("20305|エンスト||Abwürgen, Absterben des Motors||0"));
            lesson.Words.Add(new Word("20306|べんきょうになる|勉強になる|lehrreich sein, aufschlussreich sein, informativ sein||11"));
            lesson.Words.Add(new Word("20307|はっぴょうする|発表する|veröffentlichen, ankündigen, bekanntmachen||3"));
            lesson.Words.Add(new Word("20308|ちょっとよろしいでしょうか。||hast du einen Moment?||11"));
            lesson.Words.Add(new Word("20309|このあいだ|この間|neulich, vor kurzem, letztes Mal||6"));
            lesson.Words.Add(new Word("20310|ハンドル||Lenkrad||0"));
            lesson.Words.Add(new Word("20311|バックミラー||Rückspiegel||0"));
            lesson.Words.Add(new Word("20312|カーナビ||Navigationssystem||0"));
            lesson.Words.Add(new Word("20313|わせいえいご|和製英語|japanisches Englisch, in Japan eingeprägtes englisches Wort||0"));
            lesson.Words.Add(new Word("20314|アメリカンコーヒー||Amerikanischer Kaffee ( = Schwacher Kaffee)||0"));
            lesson.Words.Add(new Word("20315|うんてんめんきょ|運転免許|Führerschein||0"));
            lesson.Words.Add(new Word("20316|なるほど||achsoo!!, ahh verstehe!!, wirklich?! allerdings!!||11"));
            lesson.Words.Add(new Word("20317|リサーチ||Forschung, Untersuchung||0"));
            lesson.Words.Add(new Word("20318|ねんだい|年代|Zeitalter, Periode, Ära||0"));
            lesson.Words.Add(new Word("20319|きじ|記事|Artikel||0"));
            lesson.Words.Add(new Word("20320|カーソル||Mauscursor||0"));
        }

        private void FillGenkiL5Kanji(Lesson lesson)
        {
            lesson.Kanjis = new List<Kanji>();

            lesson.Kanjis.Add(new Kanji("10033|山|Berg|サン|やま|富士山 - ふじさん - Berg Fuji|-"));
            lesson.Kanjis.Add(new Kanji("10034|川|Fluss|セン|かわ、がわ|小川さん - おがわさん - Herr/Frau Ogawa|-"));
            lesson.Kanjis.Add(new Kanji("10035|元|Herkunft, Anfang|ゲン、ガン|もと|元気 - げんき - Gesund|-"));
            lesson.Kanjis.Add(new Kanji("10036|気|Geist, Seele, Strom|キ、ケ|いき|電気 - でんき - Elektrizität|-"));
            lesson.Kanjis.Add(new Kanji("10037|天|Himmel|テン|あめ、あま|天気 - てんき - Wetter|-"));
            lesson.Kanjis.Add(new Kanji("10038|私|Ich|シ|わたし|私立大学 - しりつだいがく - private Universität|-"));
            lesson.Kanjis.Add(new Kanji("10039|今|jetzt|コン|いま|今日 - きょう - Heute|-"));
            lesson.Kanjis.Add(new Kanji("10040|田|reisfeld|デン|た、だ|田地 - でんち - Farmland|-"));
            lesson.Kanjis.Add(new Kanji("10041|女|Frau|ジョ|おんな|女性 - じょせい - weiblich, weiblichkeit|-"));
            lesson.Kanjis.Add(new Kanji("10042|男|Mann|ダン|おとこ|男性 - だんせい - männlich, männlichkeit|-"));
            lesson.Kanjis.Add(new Kanji("10043|見|sehen|ケン|み|見物 - けんぶつ - sightseeing|-"));
            lesson.Kanjis.Add(new Kanji("10044|行|gehen, laufen, ausführen|コウ、ギョウ|い|銀行 - ぎんこう - Bank|-"));
            lesson.Kanjis.Add(new Kanji("10045|食|essen|ショク|た、く|食事 - しょくじ - Mahlzeit|-"));
            lesson.Kanjis.Add(new Kanji("10046|飲|trinken|イン|の|飲食 - いんしょく - Essen und Trinken|-"));
        }

        private void FillGenkiL6Kanji(Lesson lesson)
        {
            lesson.Kanjis = new List<Kanji>();

            lesson.Kanjis.Add(new Kanji("10047|東|Osten|トウ|ひがし|東京 - とうきょう - Tokyo|-"));
            lesson.Kanjis.Add(new Kanji("10048|西|Westen|セイ、サイ|にし|西暦 - せいれき - Gregorianischer Kalender|-"));
            lesson.Kanjis.Add(new Kanji("10049|南|Süden|ナン|みなみ|南米 - なんべい - Südamerika|-"));
            lesson.Kanjis.Add(new Kanji("10050|北|Norden|ホク、ホッ|きた|北海道 - ほっかいどう - Hokkaido|-"));
            lesson.Kanjis.Add(new Kanji("10051|口|Mund|コウ|くち、ぐち|人口 - じんこう - Bevölkerung|-"));
            lesson.Kanjis.Add(new Kanji("10052|出|verlassen, aussteigen|シュツ、シュッ|で、だ|日の出 - ひので - Sonnenaufgang|-"));
            lesson.Kanjis.Add(new Kanji("10053|右|Rechts|ウ、ユウ|みぎ|右岸 - うがん - rechtes Ufer|-"));
            lesson.Kanjis.Add(new Kanji("10054|左|Links|サ|ひだり|左折 - させつ - links abbiegen|-"));
            lesson.Kanjis.Add(new Kanji("10055|分|minute, teilen, Teil|フン、ブン、プン、ビ|わ|自分 - じぶん - selbst|-"));
            lesson.Kanjis.Add(new Kanji("10056|先|bevor, in Zukunft|セン|さき|先週 - せんしゅう - letzte Woche|-"));
            lesson.Kanjis.Add(new Kanji("10057|生|leben, wachsen, geboren sein, roh|セイ、ショウ|う|学生 - がくせい - Student|-"));
            lesson.Kanjis.Add(new Kanji("10058|大|groß|ダイ、タイ|おお|大学 - だいがく - Universität|-"));
            lesson.Kanjis.Add(new Kanji("10059|学|lernen, Schule, Wissenschaft|ガク、ガッ|まな|科学者 - かがくしゃ - Wissenschaftler|-"));
            lesson.Kanjis.Add(new Kanji("10060|外|außen, anderer, Fremder|ガイ、ゲ|そと|外国人 - がいこくじん - Ausländer|-"));
            lesson.Kanjis.Add(new Kanji("10061|国|Land|コク、ゴク|くに|国籍 - こくせき - Nationalität|-"));
        }

        private void FillMinnaNoNihongoFukushuuJ(Lesson lesson)
        {
            lesson.Clozes = new List<Cloze>();

            lesson.Clozes.Add(new Cloze("10|昨日の新聞_、日本の女性は世界_一番長生きする_。|によると_で_そうです|に…__そう…"));
            lesson.Clozes.Add(new Cloze("20|ミラーさん_いらっしゃいますか。ミラーさん_たった今帰った_です。|は_は_ところ|__"));
            lesson.Clozes.Add(new Cloze("30|昨日速達_送りました_、今日届くはずです。|で_から|_"));
        }

        #endregion
    }
}
