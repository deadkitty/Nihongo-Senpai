using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenpaiClientTest
{
    public class Lesson
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Type { get; set; }
        public int Size { get; set; }
        
        public virtual List<Word> Words { get; set; }
        
        public Lesson()
        {
            Words = new List<Word>();
        }

        public Lesson(String text)
        {
            Words = new List<Word>();
            Fill(text);
        }

        public void Fill(String properties)
        {
            String[] parts = properties.Split('|');

            if (parts.Length == 4)
            {
                Id = Convert.ToInt32(parts[0]);
                Name = parts[1];
                Type = Convert.ToInt32(parts[2]);
                Size = Convert.ToInt32(parts[3]);
            }
            else
            {
                Name = parts[0];
                Type = Convert.ToInt32(parts[1]);
                Size = Convert.ToInt32(parts[2]);
            }
        }

        public override string ToString()
        {
            return Id + "|" + Name + "|" + Type + "|" + Size;
        }

        #region TestLesson
        
        public static Lesson GetTestLesson()
        {
            #region LessonText

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Tobira L3|0|84");
            sb.AppendLine("ぎじゅつ|技術|Technologie, Technik, Fähigkeit||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("はったつする|発達する|entwickeln, wachsen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("かいじょう|会場|Versammlungsort||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("にがおえ|似顔絵|Portrait, Abbild||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("かく|描く|malen, zeichnen||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("くも、クモ|蜘蛛|Spinne||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("てんじょう|天井|Zimmerdecke||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("しゅじゅつ|手術|Operation, chirurgischer Eingriff|～をする|0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("すでに|既に|bereits, vorher||6|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("じっさいに|実際に|tatsächlich, in Wahrheit||11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("しゃかい|社会|Gesellschaft, Welt, Öffentlichkeit||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("かつやくする|活躍する|tätig sein, eine aktive/wichtige Rolle spielen, wirksam sein||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("留守番をする|留守番をする|das Haus Hüten|während eine Person nicht da ist|11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("はこぶ|運ぶ|tragen, transportieren||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("にんげん|人間|Mensch||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("くらす|暮らす|leben||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("とし|年|Jahr, Alter, jahre alt||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ケアハウス||Altenheim||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("あざらし、アザラシ|海豹|Seehund||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("け|毛|Haar, Fell, Federn, Behaarung||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("さわる|触る|berühren, fühlen|etw.|1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("くび|首|Nacken||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("うごかす|動かす|bewegen|etw.|1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("こえ|声|Stimme||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("まわり|周り|Umgebung, Umfeld, Nähe, Nachbarschaft||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("あつまる|集まる|sammeln||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("どうぶつ|動物|Tier||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("アレルギー||Allergie||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("だいじょうぶ|大丈夫|sicher, alles in Ordnung||5|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("あっ||ahh!!, ohh!!||11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("だいじ|大事|wichtig||5|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("こうか|効果|Wirksamkeit, Effektivität, Resultat,Effekt||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ギネスブック||Guinnesbuch der Rekorde||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("のる|載る|berichtet werden, erscheinen|in nem Magazin, etc.|1|2,5|0|0|2,5|0|0|13|0|0");
            sb.AppendLine("にゅうがくする|入学する|in die Schule Eintretten, immatrikulieren||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ごうかくする|合格する|bestehen|einen Test, etc.|3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("おいわい|お祝い|Glückwunsch, Gratulation||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("はじめて|初めて|zum ersten Mal||6|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("さいしょ|最初|als erstes||6|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("さいしょ|最初|der Anfang||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("つける||bezeichnen, benennen|名前を～|2|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("りかいする|理解する|verstehen,  begreifen, erfassen, einsehen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("うごく|動く|sich bewegen, gehen, funktionieren||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ざんねん|残念|bedauerlich, bedauernswert, enttäuschend, ärgerlich||5|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("これから||von jetzt an, in Zukunft||6|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("うまれる|生まれる|geboren werden, zur Welt kommen||2|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("えんそうする|演奏する|spielen, vortragen, darbieten|Musik|3|2,5|0|0|2,5|0|0|13|0|0");
            sb.AppendLine("がくしゅうする|学習する|lernen, studieren||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("しょくじする|食事する|eine Mahlzeit haben||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("さいこう|最高|das Höchste, das Beste, das Maximum, das Größte||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("うけつけ|受付|Rezeption, Auskunft, Empfang||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("あんないする|案内する|jmd. führen, geleiten, begleiten, benachrichtigen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("げんそく|原則|Prinzip, Grundsatz, Regel||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("けがする|怪我する|verletzt werden, sich verletzen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("いはんする|違反する|ein Vergehen begehen, übertreten, verletzten, verstoßen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("まもる|守る|beschützen, verteidigen||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("もんだい|問題|Problem, Frage, Fehler||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("いらいする|依頼する|beauftragen mit, bitten um, sich verlassen auf||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("かんしゃする|感謝する|danken, dankbar sein||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ホームステイさき|ホームステイ先|Gastfamilie||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("たのむ|頼む|bitten, um einen Gefallen bitten, bestellen||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("～くん|～君|Namenserweiterung für männer von gleichen oder niedrigeren Status||9|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("まちがう|間違う|einen Fehler machen||1|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ハイブリッド（しゃ）|ハイブリッド（車）|Hybridauto||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("エコカー||Umweltfreundliches Auto||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("チェックする||etw. checken/überprüfen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("はつおんする|発音する|aussprechen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("もと|元|Wurzel, Ursprung||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("エンスト||Abwürgen, Absterben des Motors||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("べんきょうになる|勉強になる|lehrreich sein, aufschlussreich sein, informativ sein||11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("はっぴょうする|発表する|veröffentlichen, ankündigen, bekanntmachen||3|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ちょっとよろしいでしょうか。||hast du einen Moment?||11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("このあいだ|この間|neulich, vor kurzem, letztes Mal||6|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ハンドル||Lenkrad||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("バックミラー||Rückspiegel||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("カーナビ||Navigationssystem||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("わせいえいご|和製英語|japanisches Englisch, in Japan eingeprägtes englisches Wort||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("アメリカンコーヒー||Amerikanischer Kaffee ( = Schwacher Kaffee)||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("うんてんめんきょ|運転免許|Führerschein||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("なるほど||achsoo!!, ahh verstehe!!, wirklich?! allerdings!!||11|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("リサーチ||Forschung, Untersuchung||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("ねんだい|年代|Zeitalter, Periode, Ära||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("きじ|記事|Artikel||0|2,5|0|0|2,5|0|0|15|0|0");
            sb.AppendLine("カーソル||Mauscursor||0|2,5|0|0|2,5|0|0|15|0|0");

            #endregion

            String text = sb.ToString();

            String[] lines = text.Split('\n');

            Lesson lesson = new Lesson(lines[0]);

            for(int i = 1; i < lines.Length - 1; ++i)
            {
                Word word = new Word(lines[i]);
                //word.lesson = lesson;
                lesson.Words.Add(word);
            }

            lesson.Size = lesson.Words.Count;

            return lesson;
        }

        #endregion
    }
}
