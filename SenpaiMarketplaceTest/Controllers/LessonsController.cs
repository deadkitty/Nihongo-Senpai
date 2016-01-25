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
            lessons.Add(new Lesson("77|Tobira L2|0|73"));
            lessons.Add(new Lesson("79|Tobira L3|0|84"));

            lessons.Add(new Lesson("55|Genki L5 Kanji|3|14"));
            lessons.Add(new Lesson("56|Genki L6 Kanji|3|15"));
            
            lessons.Add(new Lesson("51|Minna no Nihongo Fukushuu J Test|1|3"));

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
                case 77: FillTobiraL2(lesson); break;
                case 79: FillTobiraL3(lesson); break;
                case 55: FillGenkiL5Kanji(lesson); break;
                case 56: FillGenkiL6Kanji(lesson); break;
                case 51: FillMinnaNoNihongoFukushuuJ(lesson); break;
            }
            
            return Ok(lesson);
        }

        #region Fill Test Lessons

        private void FillTobiraL1(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("2055|ききかえす|聞き返す|eine Frage aufwerfen über etwas das gerade gesagt wurde|に～|1"));
            lesson.Words.Add(new Word("2056|むかしばなし|昔話|alte Geschichte, Märchen||0"));
            lesson.Words.Add(new Word("2057|せんこう|専攻|Fachgebiet, Hauptfach|を～ ... する|0"));
            lesson.Words.Add(new Word("2058|いろいろ|色々|viele verschiedene|～と|6"));
            lesson.Words.Add(new Word("2059|せんもん|専門|Hauptfach||0"));
            lesson.Words.Add(new Word("2060|ちほう|地方|Region, Gebiet, Land||0"));
            lesson.Words.Add(new Word("2061|いなか|田舎|Landschaft, Landgebiet||0"));
            lesson.Words.Add(new Word("2062|そのほか|その他|auserdem, daneben|～に|11"));
            lesson.Words.Add(new Word("2063|かんとう|関東|Kanto (Region)|～（地方）|0"));
            lesson.Words.Add(new Word("2064|かんさい|関西|Kansai (Region)|～（地方）|0"));
            lesson.Words.Add(new Word("2065|まわり|周り|Umgebung, Umfeld, Nähe, Nachbarschaft||0"));
            lesson.Words.Add(new Word("2066|はいる|入る|hereinkommen/gehen, beitreten, eingebunden werden|が～|1"));
            lesson.Words.Add(new Word("2067|ないよう|内容|Inhalt||0"));
            lesson.Words.Add(new Word("2068|ちめい|地名|Ortsname||0"));
            lesson.Words.Add(new Word("2069|めいぶつ|名物|bekanntes Produkt, lokale Spezialität||0"));
            lesson.Words.Add(new Word("2070|かんけいがある|関係がある|im zusammenhang mit ..., eine Verbindung haben mit ...|に/と～|11"));
            lesson.Words.Add(new Word("2071|でんとうてき|伝統的|traditionell||5"));
            lesson.Words.Add(new Word("2072|ぎょうじ|行事|Event||0"));
            lesson.Words.Add(new Word("2073|とし|年|Jahr||0"));
            lesson.Words.Add(new Word("2074|とくべつ|特別|speziell, besonders||5"));
            lesson.Words.Add(new Word("2075|おこなう|行う|betreiben, durchführen|を～|1"));
            lesson.Words.Add(new Word("2076|しょうがつ|正月|Neujahr|お～|0"));
            lesson.Words.Add(new Word("2077|そういう||(dinge) wie dieses, diese Sorte von (geschichten)||8"));
            lesson.Words.Add(new Word("2078|～たち|～達|Pluralmarker für Personen||10"));
            lesson.Words.Add(new Word("2079|そんな||dieses da, wie das|beim Sprecher|8"));
            lesson.Words.Add(new Word("2080|いっぱんてき|一般的|generell, üblich||5"));
            lesson.Words.Add(new Word("2081|かみしばい|紙芝居|Papierbilder-Schaukastentheater|Erzählen von Märchen anhand von Bildern auf der Straße|0"));
            lesson.Words.Add(new Word("2082|え|絵|Bild||0"));
            lesson.Words.Add(new Word("2083|たいへん|大変|sehr, überaus, auserordentlich||6"));
            lesson.Words.Add(new Word("2084|ほんとう|本当|wirklich, wahr, real|の-Ａｄｊ|8"));
            lesson.Words.Add(new Word("2085|しゅっしん|出身|jemandes Ursprung (stadt, land, etc.)||0"));
            lesson.Words.Add(new Word("2086|しぜん|自然|Natur||0"));
            lesson.Words.Add(new Word("2087|きびしい|厳しい|streng, ernst||4"));
            lesson.Words.Add(new Word("2088|きもち|気持ち|Gefühl, Stimmung, Empfinden||0"));
            lesson.Words.Add(new Word("2089|つゆ|梅雨|Regensaison in Japan||0"));
            lesson.Words.Add(new Word("2090|はじめ|初め|der Anfang||0"));
            lesson.Words.Add(new Word("2091|しつど|湿度|Feuchtigkeit||0"));
            lesson.Words.Add(new Word("2092|うまい|旨い|Lecker, Köstlich|meistens von Männern benutzt|4"));
            lesson.Words.Add(new Word("2093|ジャガいも|ジャガ芋|Kartoffel||0"));
            lesson.Words.Add(new Word("2094|とうほくちほう|東北地方|Tohoku Region||0"));
            lesson.Words.Add(new Word("2095|まつり|祭り|Fest, Festival||0"));
            lesson.Words.Add(new Word("2096|すごい|凄い|Erstaunlich, Krass, toll, schrecklich, entsetzlich||4"));
            lesson.Words.Add(new Word("2097|かんこうきゃく|観光客|Tourist||0"));
            lesson.Words.Add(new Word("2098|ぼく|僕|Ich|meist von Männern benutzt|0"));
            lesson.Words.Add(new Word("2099|おすすめ|お勧め|jemandes Empfehlung||0"));
            lesson.Words.Add(new Word("2100|ちり|地理|Geografie||0"));
            lesson.Words.Add(new Word("2101|みなさん|皆さん|jeder, jedermann||0"));
            lesson.Words.Add(new Word("2102|おおき|大き|groß, riesig|nur als Nomen Modifizierer|5"));
            lesson.Words.Add(new Word("2103|しま|島|Insel||0"));
            lesson.Words.Add(new Word("2104|たいりく|大陸|Kontinent||0"));
            lesson.Words.Add(new Word("2105|しまぐに|島国|Inselstaat||0"));
            lesson.Words.Add(new Word("2106|とし|都市|Stadt||0"));
            lesson.Words.Add(new Word("2107|こくど|国土|Territorium, Land||0"));
            lesson.Words.Add(new Word("2108|ほっかいどう|北海道|Hokkaido|Japans nördlichste Insel|0"));
            lesson.Words.Add(new Word("2109|ほんしゅう|本州|Honshuu|Japans größte Insel|0"));
            lesson.Words.Add(new Word("2110|しこく|四国|Shikoku|kleinste Insel der 4 Hauptinseln|0"));
            lesson.Words.Add(new Word("2111|きゅうしゅう|九州|Kyuushuu|Japans 3. Größte Insel|0"));
            lesson.Words.Add(new Word("2112|～いじょう|～以上|mehr als ..., über ...||9"));
            lesson.Words.Add(new Word("2113|ぜんたい|全体|gesamt, ganz, generell||8"));
            lesson.Words.Add(new Word("2114|２５ふんの１|２５分の１|1/25||11"));
            lesson.Words.Add(new Word("2115|と/どう/ふ/けん|都/道/府/県|Präfektur|4 stk.|0"));
            lesson.Words.Add(new Word("2116|しゅと|首都|Hauptstadt||0"));
            lesson.Words.Add(new Word("2117|そのほか|その他|der rest, die anderen||11"));
            lesson.Words.Add(new Word("2118|ふじさん|富士山|Fujiyama||0"));
            lesson.Words.Add(new Word("2119|せんそう|戦争|Krieg||0"));
            lesson.Words.Add(new Word("2120|おそろしい|恐ろしい|furchteregend, erschreckend||4"));
            lesson.Words.Add(new Word("2121|へいわ|平和|Frieden||0"));
            lesson.Words.Add(new Word("2122|つたえる|伝える|mitteilen, erzählen, informieren|～に、～を|2"));
            lesson.Words.Add(new Word("2123|げんばくドーム|原爆ドーム|Atombombenkuppel|広島|0"));
            lesson.Words.Add(new Word("2124|なんぼく|南北|Norden und Süden||0"));
            lesson.Words.Add(new Word("2125|きこう|気候|Klima||0"));
            lesson.Words.Add(new Word("2126|ひ|日|Tag||0"));
            lesson.Words.Add(new Word("2127|きおん|気温|(atmosphärische)Temperatur||0"));
            lesson.Words.Add(new Word("2128|さ|差|Differenz, Unterschied||0"));
            lesson.Words.Add(new Word("2129|せっし|摂氏|Hundertteilig(zb. cm), Celsius||0"));
            lesson.Words.Add(new Word("2130|さくら|桜|Kirschblüten||0"));
            lesson.Words.Add(new Word("2131|おわり|終わり|das Ende||0"));
            lesson.Words.Add(new Word("2132|ひとびと|人々|Leute||0"));
            lesson.Words.Add(new Word("2133|はなみ|花見|Hanami||0"));
            lesson.Words.Add(new Word("2134|たのしむ|楽しむ|genießen||1"));
            lesson.Words.Add(new Word("2135|めいしょ|名所|bekannter/berühmter Ort||0"));
            lesson.Words.Add(new Word("2136|たとえば|例えば|zum Beispiel||6"));
            lesson.Words.Add(new Word("2137|もっとも|最も|meistens, äußerst||6"));
            lesson.Words.Add(new Word("2138|うつくしい|美しい|schön, hübsch||4"));
            lesson.Words.Add(new Word("2139|しろ|城|Burg|お～|0"));
            lesson.Words.Add(new Word("2140|ユネスコ||UNESCO||0"));
            lesson.Words.Add(new Word("2141|せかいいさん|世界遺産|Welterbe, Weltkulturerbe||0"));
            lesson.Words.Add(new Word("2142|～まえ|～前|vor ...|Jahre, Tage, etc.|9"));
            lesson.Words.Add(new Word("2143|かべ|壁|Wand||0"));
            lesson.Words.Add(new Word("2144|のこる|残る|bleiben, übrig bleiben||1"));
            lesson.Words.Add(new Word("2145|たてもの|建物|Gebäude||0"));
            lesson.Words.Add(new Word("2146|かたち|形|Form, Aussehen, Erscheinung||0"));
            lesson.Words.Add(new Word("2147|しらさぎ|白鷺|Silberreiher|Vogel|0"));
            lesson.Words.Add(new Word("2148|はね|羽|Flügel||0"));
            lesson.Words.Add(new Word("2149|ひろげる|広げる|ausbreiten, erweitern, breiter machen||2"));
            lesson.Words.Add(new Word("2150|さつえいする|撮影する|filmen, Dreharbeiten machen||3"));
            lesson.Words.Add(new Word("2151|かざん|火山|Vulkan||0"));
            lesson.Words.Add(new Word("2152|かんこうする|観光する|Sightseeing, besichtigen||3"));
            lesson.Words.Add(new Word("2153|レジャー||Freizeit||0"));
            lesson.Words.Add(new Word("2154|もくてき|目的|Zweck, Grund||0"));
            lesson.Words.Add(new Word("2155|ゆかた|浴衣|Yukata|informeller Baumwollkimono für den Sommer|0"));
            lesson.Words.Add(new Word("2156|リラックスする||relaxen||3"));
            lesson.Words.Add(new Word("2157|けしき|景色|Aussicht, Landschaft, Kulisse||0"));
            lesson.Words.Add(new Word("2158|とくに|特に|speziell, insbesondere, besonders||6"));
            lesson.Words.Add(new Word("2159|し|市|Kleinstadt, Gemeinde||0"));
            lesson.Words.Add(new Word("2160|～かい|～階|Stockwerk||0"));
            lesson.Words.Add(new Word("2161|ま|間|Raum||0"));
            lesson.Words.Add(new Word("2162|けんがくする|見学する|Besichtigen||3"));
            lesson.Words.Add(new Word("2163|みやげばなし|土産話|Reise- Anekdote/Geschichte||0"));
        }

        private void FillTobiraL2(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("2164|あやまる|謝る|sich entschuldigen||1"));
            lesson.Words.Add(new Word("2165|ごめんなさい||Entschuldigung||11"));
            lesson.Words.Add(new Word("2166|かちょう|課長|Sektionsleiter||0"));
            lesson.Words.Add(new Word("2167|～すぎ|～過ぎ|nach~, über~, zu viel~||9"));
            lesson.Words.Add(new Word("2168|むりをする|無理をする|sich überanstrengen, etw. Schwieriges versuchen||3"));
            lesson.Words.Add(new Word("2169|おせわになる|お世話になる|jmd. zur Last fallen/Umstände bereiten||11"));
            lesson.Words.Add(new Word("2170|げんご|言語|Sprache||0"));
            lesson.Words.Add(new Word("2171|ひょうげん|表現|Ausdruck, Formulierung, Darstellung||0"));
            lesson.Words.Add(new Word("2172|りゆう|理由|Grund, Zweck||0"));
            lesson.Words.Add(new Word("2173|じょうきょう|状況|Situation||0"));
            lesson.Words.Add(new Word("2174|ふくさつ|複雑|kompliziert||5"));
            lesson.Words.Add(new Word("2175|じつは|実は|Es ist so das ..., Ehrlich gesagt ...||11"));
            lesson.Words.Add(new Word("2176|きまり|決まり|Festlegung, Entscheidung, Regel, Vorschrift||0"));
            lesson.Words.Add(new Word("2177|あいて|相手|Partner, Kamerad, die Person zu der jmd. spricht||0"));
            lesson.Words.Add(new Word("2178|かえる|変える|ändern, verändern||2"));
            lesson.Words.Add(new Word("2179|ぶぶん|部分|Teil, Abschnitt||0"));
            lesson.Words.Add(new Word("2180|だんせい|男性|männlich||0"));
            lesson.Words.Add(new Word("2181|じょせい|女性|weiblich||0"));
            lesson.Words.Add(new Word("2182|くらべる|比べる|vergleichen||2"));
            lesson.Words.Add(new Word("2183|かんじ|感じ|Gefühl, Empfindung, Eindruck||0"));
            lesson.Words.Add(new Word("2184|おおくの|多くの|viel, zahlreich, die meisten, der größte Teil von ...||8"));
            lesson.Words.Add(new Word("2185|ばめん|場面|Schauplatz, Situation, Szene||0"));
            lesson.Words.Add(new Word("2186|かんさつ|観察|Beobachtung, Beaufsichtigung, Überwachung|～する|0"));
            lesson.Words.Add(new Word("2187|ひょう|表|Tabelle, Diagramm||0"));
            lesson.Words.Add(new Word("2188|だんじょ|男女|Mann und Frau||0"));
            lesson.Words.Add(new Word("2189|おごる|奢る|jmd. einladen/bewirten|zum Essen, Tee, etc.|1"));
            lesson.Words.Add(new Word("2190|もじ|文字|Schriftzeichen, Buchstabe||0"));
            lesson.Words.Add(new Word("2191|こいびと|恋人|Freund/Freundin||0"));
            lesson.Words.Add(new Word("2192|れい|例|Beispiel||0"));
            lesson.Words.Add(new Word("2193|さいご|最後|Ende, Schluss||0"));
            lesson.Words.Add(new Word("2194|れんらく|連絡|Kontakt, Kommunikation|～する|0"));
            lesson.Words.Add(new Word("2195|はっきり||klar, deutlich||6"));
            lesson.Words.Add(new Word("2196|さそう|誘う|einladen, fragen|jmd. zu ihrgentwas ...|1"));
            lesson.Words.Add(new Word("2197|ことわる|断る|ablehnen, absagen, verweigern||1"));
            lesson.Words.Add(new Word("2198|つごうがわるい|都合が悪い|ungünstig sein, nicht passen, ungelegen sein||4"));
            lesson.Words.Add(new Word("2199|きぶん|気分|Stimmung, Verfassung, Laune||0"));
            lesson.Words.Add(new Word("2200|おねがい|お願い|Wunsch, Hoffnung, Bitte|～する|0"));
            lesson.Words.Add(new Word("2201|たいせつにする|大切にする|schätzen, achten, sorgsam mit etw. umgehen||3"));
            lesson.Words.Add(new Word("2202|かんたん|簡単|leicht, einfach||5"));
            lesson.Words.Add(new Word("2203|なれる|慣れる|sich gewöhnen, sich anpassen, vertraut werden||2"));
            lesson.Words.Add(new Word("2204|とくちょう|特徴|Eigentümlichkeit, Besonderheit, charakteristisches Merkmal||0"));
            lesson.Words.Add(new Word("2205|ひつよう|必要|Notwendigkeit, Bedürfniss||0"));
            lesson.Words.Add(new Word("2206|けいたいでんわ|携帯電話|Handy||0"));
            lesson.Words.Add(new Word("2207|ふつう|普通|Normalität, Durchschnittlichkeit||0"));
            lesson.Words.Add(new Word("2208|ろんぶん|論文|Aufsatz, Artikel, Arbeit, Dissetation, ...||0"));
            lesson.Words.Add(new Word("2209|あう|合う|passen, übereinstimmen||1"));
            lesson.Words.Add(new Word("2210|けいご|敬語|Höflicher Ausdruck, Höflichkeitsform||0"));
            lesson.Words.Add(new Word("2211|か|課|Lektion||0"));
            lesson.Words.Add(new Word("2212|ていねい|丁寧|höflich, respektvoll, gründlich, sorgfältig||5"));
            lesson.Words.Add(new Word("2213|くだけた|砕けた|einfach, entspannt, informell||8"));
            lesson.Words.Add(new Word("2214|つかいわける|使い分ける|etw. angemessen benutzen||2"));
            lesson.Words.Add(new Word("2215|ぶんまつ|文末|Satzende||0"));
            lesson.Words.Add(new Word("2216|あらわれる|現れる|erscheinen, auftauchen||2"));
            lesson.Words.Add(new Word("2217|あのかた|あの方|diese Person|höflich|11"));
            lesson.Words.Add(new Word("2218|あいつ|彼奴|der Typ da|vulgär|0"));
            lesson.Words.Add(new Word("2219|おれ|俺|Ich|von Männern benutzt|0"));
            lesson.Words.Add(new Word("2220|このへんに|この辺に|hier in der Gegend||11"));
            lesson.Words.Add(new Word("2221|ずいぶん|随分|viel, ziemlich, sehr||6"));
            lesson.Words.Add(new Word("2222|しょうりゃくする|省略する|auslassen, abkürzen, weglassen||3"));
            lesson.Words.Add(new Word("2223|たんしゅくけい|短縮形|verkürzte Form||0"));
            lesson.Words.Add(new Word("2224|はなしことば|話言葉|gesprochene Sprache||0"));
            lesson.Words.Add(new Word("2225|こういう||von dieser sorte, wie dieses||11"));
            lesson.Words.Add(new Word("2226|とうち|倒置|Umkehrung, Inversion, Wechsel||0"));
            lesson.Words.Add(new Word("2227|このような||so ein ..., solch ein ...||11"));
            lesson.Words.Add(new Word("2228|かきことば|書き言葉|geschriebene Sprache||0"));
            lesson.Words.Add(new Word("2229|～たい|～体|Style|im Sprachgebrauch|9"));
            lesson.Words.Add(new Word("2230|きょうみぶかい|興味深い|sehr interessant||4"));
            lesson.Words.Add(new Word("2231|かきことばてき|書き言葉的|wie geschriebene sprache ...||5"));
            lesson.Words.Add(new Word("2232|～てき|～的|...lich, ...isch, ...mäßig||9"));
            lesson.Words.Add(new Word("2233|おたく|お宅|jemandes Zuhause|höflich|0"));
            lesson.Words.Add(new Word("2234|それでは||dann, wenn das so ist, in dem fall||11"));
            lesson.Words.Add(new Word("2235|しょうしょう|少々|ein wenig, ein bisschen|formell|6"));
            lesson.Words.Add(new Word("2236|きゅう|急|plötzlich, auf einmal||5"));
        }

        private void FillTobiraL3(Lesson lesson)
        {
            lesson.Words = new List<Word>();

            lesson.Words.Add(new Word("2237|ぎじゅつ|技術|Technologie, Technik, Fähigkeit||0"));
            lesson.Words.Add(new Word("2238|はったつする|発達する|entwickeln, wachsen||3"));
            lesson.Words.Add(new Word("2239|かいじょう|会場|Versammlungsort||0"));
            lesson.Words.Add(new Word("2240|にがおえ|似顔絵|Portrait, Abbild||0"));
            lesson.Words.Add(new Word("2241|かく|描く|malen, zeichnen||1"));
            lesson.Words.Add(new Word("2242|くも、クモ|蜘蛛|Spinne||0"));
            lesson.Words.Add(new Word("2243|てんじょう|天井|Zimmerdecke||0"));
            lesson.Words.Add(new Word("2244|しゅじゅつ|手術|Operation, chirurgischer Eingriff|～をする|0"));
            lesson.Words.Add(new Word("2245|すでに|既に|bereits, vorher||6"));
            lesson.Words.Add(new Word("2246|じっさいに|実際に|tatsächlich, in Wahrheit||11"));
            lesson.Words.Add(new Word("2247|しゃかい|社会|Gesellschaft, Welt, Öffentlichkeit||0"));
            lesson.Words.Add(new Word("2248|かつやくする|活躍する|tätig sein, eine aktive/wichtige Rolle spielen, wirksam sein||3"));
            lesson.Words.Add(new Word("2249|留守番をする|留守番をする|das Haus Hüten|während eine Person nicht da ist|11"));
            lesson.Words.Add(new Word("2250|はこぶ|運ぶ|tragen, transportieren||1"));
            lesson.Words.Add(new Word("2251|にんげん|人間|Mensch||0"));
            lesson.Words.Add(new Word("2252|くらす|暮らす|leben||1"));
            lesson.Words.Add(new Word("2253|とし|年|Jahr, Alter, jahre alt||0"));
            lesson.Words.Add(new Word("2254|ケアハウス||Altenheim||0"));
            lesson.Words.Add(new Word("2255|あざらし、アザラシ|海豹|Seehund||0"));
            lesson.Words.Add(new Word("2256|け|毛|Haar, Fell, Federn, Behaarung||0"));
            lesson.Words.Add(new Word("2257|さわる|触る|berühren, fühlen|etw.|1"));
            lesson.Words.Add(new Word("2258|くび|首|Nacken||0"));
            lesson.Words.Add(new Word("2259|うごかす|動かす|bewegen|etw.|1"));
            lesson.Words.Add(new Word("2260|こえ|声|Stimme||0"));
            lesson.Words.Add(new Word("2261|まわり|周り|Umgebung, Umfeld, Nähe, Nachbarschaft||0"));
            lesson.Words.Add(new Word("2262|あつまる|集まる|sammeln||1"));
            lesson.Words.Add(new Word("2263|どうぶつ|動物|Tier||0"));
            lesson.Words.Add(new Word("2264|アレルギー||Allergie||0"));
            lesson.Words.Add(new Word("2265|だいじょうぶ|大丈夫|sicher, alles in Ordnung||5"));
            lesson.Words.Add(new Word("2266|あっ||ahh!!, ohh!!||11"));
            lesson.Words.Add(new Word("2267|だいじ|大事|wichtig||5"));
            lesson.Words.Add(new Word("2268|こうか|効果|Wirksamkeit, Effektivität, Resultat,Effekt||0"));
            lesson.Words.Add(new Word("2269|ギネスブック||Guinnesbuch der Rekorde||0"));
            lesson.Words.Add(new Word("2270|のる|載る|berichtet werden, erscheinen|in nem Magazin, etc.|1"));
            lesson.Words.Add(new Word("2271|にゅうがくする|入学する|in die Schule Eintretten, immatrikulieren||3"));
            lesson.Words.Add(new Word("2272|ごうかくする|合格する|bestehen|einen Test, etc.|3"));
            lesson.Words.Add(new Word("2273|おいわい|お祝い|Glückwunsch, Gratulation||0"));
            lesson.Words.Add(new Word("2274|はじめて|初めて|zum ersten Mal||6"));
            lesson.Words.Add(new Word("2275|さいしょ|最初|als erstes||6"));
            lesson.Words.Add(new Word("2276|さいしょ|最初|der Anfang||0"));
            lesson.Words.Add(new Word("2277|つける||bezeichnen, benennen|名前を～|2"));
            lesson.Words.Add(new Word("2278|りかいする|理解する|verstehen,  begreifen, erfassen, einsehen||3"));
            lesson.Words.Add(new Word("2279|うごく|動く|sich bewegen, gehen, funktionieren||1"));
            lesson.Words.Add(new Word("2280|ざんねん|残念|bedauerlich, bedauernswert, enttäuschend, ärgerlich||5"));
            lesson.Words.Add(new Word("2281|これから||von jetzt an, in Zukunft||6"));
            lesson.Words.Add(new Word("2282|うまれる|生まれる|geboren werden, zur Welt kommen||2"));
            lesson.Words.Add(new Word("2283|えんそうする|演奏する|spielen, vortragen, darbieten|Musik|3"));
            lesson.Words.Add(new Word("2284|がくしゅうする|学習する|lernen, studieren||3"));
            lesson.Words.Add(new Word("2285|しょくじする|食事する|eine Mahlzeit haben||3"));
            lesson.Words.Add(new Word("2286|さいこう|最高|das Höchste, das Beste, das Maximum, das Größte||0"));
            lesson.Words.Add(new Word("2287|うけつけ|受付|Rezeption, Auskunft, Empfang||0"));
            lesson.Words.Add(new Word("2288|あんないする|案内する|jmd. führen, geleiten, begleiten, benachrichtigen||3"));
            lesson.Words.Add(new Word("2289|げんそく|原則|Prinzip, Grundsatz, Regel||0"));
            lesson.Words.Add(new Word("2290|けがする|怪我する|verletzt werden, sich verletzen||3"));
            lesson.Words.Add(new Word("2291|いはんする|違反する|ein Vergehen begehen, übertreten, verletzten, verstoßen||3"));
            lesson.Words.Add(new Word("2292|まもる|守る|beschützen, verteidigen||1"));
            lesson.Words.Add(new Word("2293|もんだい|問題|Problem, Frage, Fehler||0"));
            lesson.Words.Add(new Word("2294|いらいする|依頼する|beauftragen mit, bitten um, sich verlassen auf||3"));
            lesson.Words.Add(new Word("2295|かんしゃする|感謝する|danken, dankbar sein||3"));
            lesson.Words.Add(new Word("2296|ホームステイさき|ホームステイ先|Gastfamilie||0"));
            lesson.Words.Add(new Word("2297|たのむ|頼む|bitten, um einen Gefallen bitten, bestellen||1"));
            lesson.Words.Add(new Word("2298|～くん|～君|Namenserweiterung für männer von gleichen oder niedrigeren Status||9"));
            lesson.Words.Add(new Word("2299|まちがう|間違う|einen Fehler machen||1"));
            lesson.Words.Add(new Word("2300|ハイブリッド（しゃ）|ハイブリッド（車）|Hybridauto||0"));
            lesson.Words.Add(new Word("2301|エコカー||Umweltfreundliches Auto||0"));
            lesson.Words.Add(new Word("2302|チェックする||etw. checken/überprüfen||3"));
            lesson.Words.Add(new Word("2303|はつおんする|発音する|aussprechen||3"));
            lesson.Words.Add(new Word("2304|もと|元|Wurzel, Ursprung||0"));
            lesson.Words.Add(new Word("2305|エンスト||Abwürgen, Absterben des Motors||0"));
            lesson.Words.Add(new Word("2306|べんきょうになる|勉強になる|lehrreich sein, aufschlussreich sein, informativ sein||11"));
            lesson.Words.Add(new Word("2307|はっぴょうする|発表する|veröffentlichen, ankündigen, bekanntmachen||3"));
            lesson.Words.Add(new Word("2308|ちょっとよろしいでしょうか。||hast du einen Moment?||11"));
            lesson.Words.Add(new Word("2309|このあいだ|この間|neulich, vor kurzem, letztes Mal||6"));
            lesson.Words.Add(new Word("2310|ハンドル||Lenkrad||0"));
            lesson.Words.Add(new Word("2311|バックミラー||Rückspiegel||0"));
            lesson.Words.Add(new Word("2312|カーナビ||Navigationssystem||0"));
            lesson.Words.Add(new Word("2313|わせいえいご|和製英語|japanisches Englisch, in Japan eingeprägtes englisches Wort||0"));
            lesson.Words.Add(new Word("2314|アメリカンコーヒー||Amerikanischer Kaffee ( = Schwacher Kaffee)||0"));
            lesson.Words.Add(new Word("2315|うんてんめんきょ|運転免許|Führerschein||0"));
            lesson.Words.Add(new Word("2316|なるほど||achsoo!!, ahh verstehe!!, wirklich?! allerdings!!||11"));
            lesson.Words.Add(new Word("2317|リサーチ||Forschung, Untersuchung||0"));
            lesson.Words.Add(new Word("2318|ねんだい|年代|Zeitalter, Periode, Ära||0"));
            lesson.Words.Add(new Word("2319|きじ|記事|Artikel||0"));
            lesson.Words.Add(new Word("2320|カーソル||Mauscursor||0"));
        }

        private void FillGenkiL5Kanji(Lesson lesson)
        {
            lesson.Kanjis = new List<Kanji>();

            lesson.Kanjis.Add(new Kanji("133|山|Berg|サン|やま|富士山 - ふじさん - Berg Fuji|-"));
            lesson.Kanjis.Add(new Kanji("134|川|Fluss|セン|かわ、がわ|小川さん - おがわさん - Herr/Frau Ogawa|-"));
            lesson.Kanjis.Add(new Kanji("135|元|Herkunft, Anfang|ゲン、ガン|もと|元気 - げんき - Gesund|-"));
            lesson.Kanjis.Add(new Kanji("136|気|Geist, Seele, Strom|キ、ケ|いき|電気 - でんき - Elektrizität|-"));
            lesson.Kanjis.Add(new Kanji("137|天|Himmel|テン|あめ、あま|天気 - てんき - Wetter|-"));
            lesson.Kanjis.Add(new Kanji("138|私|Ich|シ|わたし|私立大学 - しりつだいがく - private Universität|-"));
            lesson.Kanjis.Add(new Kanji("139|今|jetzt|コン|いま|今日 - きょう - Heute|-"));
            lesson.Kanjis.Add(new Kanji("140|田|reisfeld|デン|た、だ|田地 - でんち - Farmland|-"));
            lesson.Kanjis.Add(new Kanji("141|女|Frau|ジョ|おんな|女性 - じょせい - weiblich, weiblichkeit|-"));
            lesson.Kanjis.Add(new Kanji("142|男|Mann|ダン|おとこ|男性 - だんせい - männlich, männlichkeit|-"));
            lesson.Kanjis.Add(new Kanji("143|見|sehen|ケン|み|見物 - けんぶつ - sightseeing|-"));
            lesson.Kanjis.Add(new Kanji("144|行|gehen, laufen, ausführen|コウ、ギョウ|い|銀行 - ぎんこう - Bank|-"));
            lesson.Kanjis.Add(new Kanji("145|食|essen|ショク|た、く|食事 - しょくじ - Mahlzeit|-"));
            lesson.Kanjis.Add(new Kanji("146|飲|trinken|イン|の|飲食 - いんしょく - Essen und Trinken|-"));
        }

        private void FillGenkiL6Kanji(Lesson lesson)
        {
            lesson.Kanjis = new List<Kanji>();

            lesson.Kanjis.Add(new Kanji("147|東|Osten|トウ|ひがし|東京 - とうきょう - Tokyo|-"));
            lesson.Kanjis.Add(new Kanji("148|西|Westen|セイ、サイ|にし|西暦 - せいれき - Gregorianischer Kalender|-"));
            lesson.Kanjis.Add(new Kanji("149|南|Süden|ナン|みなみ|南米 - なんべい - Südamerika|-"));
            lesson.Kanjis.Add(new Kanji("150|北|Norden|ホク、ホッ|きた|北海道 - ほっかいどう - Hokkaido|-"));
            lesson.Kanjis.Add(new Kanji("151|口|Mund|コウ|くち、ぐち|人口 - じんこう - Bevölkerung|-"));
            lesson.Kanjis.Add(new Kanji("152|出|verlassen, aussteigen|シュツ、シュッ|で、だ|日の出 - ひので - Sonnenaufgang|-"));
            lesson.Kanjis.Add(new Kanji("153|右|Rechts|ウ、ユウ|みぎ|右岸 - うがん - rechtes Ufer|-"));
            lesson.Kanjis.Add(new Kanji("154|左|Links|サ|ひだり|左折 - させつ - links abbiegen|-"));
            lesson.Kanjis.Add(new Kanji("155|分|minute, teilen, Teil|フン、ブン、プン、ビ|わ|自分 - じぶん - selbst|-"));
            lesson.Kanjis.Add(new Kanji("156|先|bevor, in Zukunft|セン|さき|先週 - せんしゅう - letzte Woche|-"));
            lesson.Kanjis.Add(new Kanji("157|生|leben, wachsen, geboren sein, roh|セイ、ショウ|う|学生 - がくせい - Student|-"));
            lesson.Kanjis.Add(new Kanji("158|大|groß|ダイ、タイ|おお|大学 - だいがく - Universität|-"));
            lesson.Kanjis.Add(new Kanji("159|学|lernen, Schule, Wissenschaft|ガク、ガッ|まな|科学者 - かがくしゃ - Wissenschaftler|-"));
            lesson.Kanjis.Add(new Kanji("160|外|außen, anderer, Fremder|ガイ、ゲ|そと|外国人 - がいこくじん - Ausländer|-"));
            lesson.Kanjis.Add(new Kanji("161|国|Land|コク、ゴク|くに|国籍 - こくせき - Nationalität|-"));
        }

        private void FillMinnaNoNihongoFukushuuJ(Lesson lesson)
        {
            lesson.Clozes = new List<Cloze>();

            lesson.Clozes.Add(new Cloze("1|昨日の新聞_、日本の女性は世界_一番長生きする_。|によると_で_そうです|に…__そう…"));
            lesson.Clozes.Add(new Cloze("2|ミラーさん_いらっしゃいますか。ミラーさん_たった今帰った_です。|は_は_ところ|__"));
            lesson.Clozes.Add(new Cloze("3|昨日速達_送りました_、今日届くはずです。|で_から|_"));
        }

        #endregion
    }
}
