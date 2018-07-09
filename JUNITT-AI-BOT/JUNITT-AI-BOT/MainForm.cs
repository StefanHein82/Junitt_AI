using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsoleWidget;
using MaterialSkin;
using MaterialSkin.Controls;
using MaterialSkin.Animations;
using System.Speech.AudioFormat;
using System.Speech.Recognition.SrgsGrammar;
using System.Speech.Recognition;
using System.Speech.Synthesis.TtsEngine;
using System.Speech.Synthesis;
using System.Speech;
using System.Net.NetworkInformation;
using System.Net;
using Newtonsoft.Json.Serialization;
using Newtonsoft;
using OpenWeatherMap;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi;
using AudioSwitcher.AudioApi.Session;
using Newtonsoft.Json;
using System.Threading;
using System.Workflow.ComponentModel.Design;
using System.IO;

namespace WindowsFormsApp14
{
    [Serializable]
    public partial class MainForm : MaterialForm
    {
        internal Boolean hören = true;
        public Boolean Suche = false;
        public Boolean Wasist = false;

        string User = SystemInformation.UserName;
        string Domain = SystemInformation.UserDomainName;

        public static int MonitorCount { get; }
        public static string ComputerName { get; }
        public static bool Network { get; }

        PerformanceCounter perfCPU = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        PerformanceCounter perfRAM = new PerformanceCounter("Memory", "Available MBytes");

        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();
        SpeechSynthesizer speech = new SpeechSynthesizer();

        JsonSerializer json = new JsonSerializer();
        WebClient client = new WebClient();

        public MainForm()
        {
            InitializeComponent();
            Timer1.Start();

            Choices commands = new Choices();
            string path = Directory.GetCurrentDirectory() + "\\commands.txt";
            commands.Add(File.ReadAllLines(path));
            GrammarBuilder builder = new GrammarBuilder(commands);
            Grammar grammar = new Grammar(builder);
            engine.LoadGrammar(grammar);
            engine.SetInputToDefaultAudioDevice();
            engine.SpeechRecognized += RecEngine_SpeechRecognized;
            engine.RecognizeAsync(RecognizeMode.Multiple);
            speech.SelectVoiceByHints(VoiceGender.Female, VoiceAge.Adult);
            speech.Volume = 0b110_0100;
            speech.Rate = 0b1;

            Random r1 = new Random();
            String[] begrüssen = new string[0b1011] { "ich bin skeihnett....ähm ich meinte junitt", "wie geht es äuch heute", "halli hallo", "guck guck", "ich bin junitt", "hallo freunde, was macht ihr so?", "endlich wieder aktiv, ich danke äuch", "input erwartet! was ist für heute geplant", "hallo zusammen, ich grüße äuch", "hällo again, ich sag einfach hällo again", "ich dachte schon du schaltest mich gar nicht mehr ein" };
            speech.SpeakAsync(begrüssen[r1.Next(0b1011)]);

        }

        void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            hören = true;

            Random r = new Random();
            String[] hallostefan = new string[7] { "kuck kuck", "hallo zusammen", "hallo", "ich grüße äuch", "habt ihr gut geschlafen", "hallo zusammen, ich grüße äuch", "möge die macht mit äuch sein" };
            String[] sinn = new string[4] { "träume dein leben und lebe deinen traum", "zweiundvierzig", "sinn des lebens ist es zu sterben", "das leben zu geniessen" };
            String[] wie = new string[6] { "ja alles super", "es geht", "mir geht es gut und wie geht es euch?", "alles bestens", "es ist mir zu ruhig hier", "ich mag äuch" };
            String[] danke = new string[4] { "gern geschehen", "immer wieder gerne", "für euch doch immer", "nicht dafür" };
            String[] konsole = new string[4] { "ja", "okay", "sofort", "öffne konsole" };
            String[] bisbald = new string[4] { "wir sehen uns", "machs gut mein freund", "wir sehen uns", "bis bald" };
            String[] zeichnen = new string[4] { "oh ich liebe kunst", "vermal dich nicht", "kreativität macht intelligent, ich möchte auch kreativ sein", "viele bunte farben" };
            String[] zeichnencls = new string[4] { "zeig mal was du gemalt hast", "ja mein freund", "kreativität macht intelligent, ich möchte auch kreativ sein", "viele bunte farben werden gelöscht" };
            String[] maximieren = new string[4] { "sofort", "befehl wird ausgeführt", "da bin ich wieder", "okay" };
            String[] systemsteuerung = new string[4] { "okay", "systemsteuerung wird ausgeführt", "wird erledigt", "wird ausgeführt" };
            String[] was = new string[4] { "ich werde ein intelligenter Computer sein aber noch lerne ich", "auf jedenfall kein mensch", "ich bin junitt", "eine elektronische rechenmaschine mit potenzial" };
            String[] wer = new string[4] { "ich bin junitt version drei", "ich bin junitt", "dein computer, dem du einen namen gabst", "junitt" };
            String[] anwendung = new string[4] { "jawohl", "sofort", "wird erledigt", "editor wird ausgeführt" };
            String[] browsercls = new string[4] { "viel spaß im internet", "hüte dich vor häckern", "ja sofort", "okay" };
            String[] universum = new string[4] { "Ich suche mal schnell, hier ich habe etwas gefunden das dich interessieren könnte", "ich habe folgendes gefunden", "suche wird ausgeführt", "mach ich sofort" };
            String[] prozesse = new string[4] { "sofort", "wird erledigt", "die performänce sollte untersucht werden", "task mänäger wird ausgeführt" };
            String[] windowsver1 = new string[3] { "siebzehn null neun", "windows 10 version eins sieben null neun", "windows 10, brauchst su auch die version?" };
            String[] windowsver = new string[3] { "fenster wird geschlossen", "wie du wünscht", "wird beendet" };
            String[] spiele = new string[4] { "viel spaß beim spielen", "öffne stiem", "na gucken wir mal nach", "schauen wir einfach mal nach" };
            String[] pokern = new string[4] { "ich drücke dir die daumen0", "spielen macht süchtig, bitte sei vorsichtig", "okay", "viel glück" };
            String[] pokerncls = new string[4] { "wird beendet", "viel glück beeim nächsten mal", "wird erledigt mein freund", "okay stefan" };
            String[] minimieren = new string[4] { "bin schon weg", "ja sofort", "bis gleich", "ja" };
            String[] registrierung = new string[4] { "wird geöffnet", "sofort...erledigt", "öffne registrierung", "okay" };
            String[] pinkyundbrain = new string[4] { "na was wohl pinky, das gleiche wie jeden abend wir versuchen die weltherrschafft an uns zu reißen", "mein input erweitern bitte", "heute wird geschlafen", "produktiv in sachen k i sein" };
            String[] neun = new string[4] { "neun", "natürlich neun, was sonst?", "quadrahtwurzel aus einundachtzig ist gleich neun mal neun, also 9", "ich mag jetzt nicht" };
            String[] mathematik = new string[4] { "ich liebe mathematik", "ohne mathematik würde es mich nicht geben", "du und deine zahlen", "ja liebend gerne" };
            String[] astronomie = new string[4] { "ich würde auch mal gerne ins all fliegen", "siehst du, du willst doch mal zum jupiter, wann machst du das endlich mal?", "informationen über astronomie werden zusammen getragen", "wird ausgeführt, einen augenblick bitte" };
            String[] computer = new string[4] { "ich such im netz", "moment bitte", "suche wird durchgeführt", "ja suche beschreibung über mich" };
            String[] chemie = new string[4] { "physik ist doch viel interessanter", "okay suche infos über chemie", "chemie ist nicht so meins aber du bist der mensch", "infos werden geladen...fertig." };
            String[] schließen = new string[4] { "editor wird geschlossen", "ja wird geschlossen", "wird beendet", "beende editor" };
            String[] physik = new string[4] { "suche informationen", "einen moment bitte", "informationen werden gesammelt", "über physik habe ich folgendes gefunden" };
            String[] geschichte = new string[4] { "suche nach infos über geschichte", "ich habe folgendes über geschichte gefunden", "geschichte", "suche im internet nach geschichte moment" };
            String[] internet = new string[4] { "firefox wird geöffnet", "ja wird erledigt", "firefox", "wird geöffnet" };
            String[] internetcls = new string[4] { "firefox wird beendet", "browser wird geschlossen", "was machen wir jetzt", "bist du denn schon fertig" };
            String[] anwendungcls = new string[4] { "wird beendet", "ja wird sofort erledigt", "wird erledigt", "editor wird beendet" };
            String[] befinden = new string[6] { "es ging mir schon mal besser aber soweit ganz gut", "es geht mir bestens", "ja alles super", "wunderbar, ich bin junitt", "berlin ist echt nicht die sauberste stadt", "mach mich mal wieder sauber" };
            String[] office = new string[3] { "achte auf deine rechtschreibung", "office wir geöffnet", "was möchdich tunodus an" };
            String[] ordner = new string[3] { "ich suche mal schnell", "scanne ordnerstruktur", "scan läuft einen moment bitte" };
            String[] abbrechen = new string[4] { "abgebrochen", "es wurde doch grad interessant", "nagut weil du es bist", "schade ich möchte weiterlesen" };
            String[] clsdownloads = new string[3] { "downloads wird geschlossen", "schliesse downloads", "schliesse ordnerfenster" };
            String[] downloads = new string[3] { "downloads wird geöffnet", "scanne ordnerstruktur und öffne downloads", "öffne downloads ordnerfenster" };
            String[] festplatte = new string[2] { "Festplatte", "c wurde geöffnet" };
            String[] desktop = new string[2] { "befinde mich auf desktop", "desktop wurde geöffnet" };
            String[] witzig = new string[8] { "warum haben ostfriesen immer absolut immer einen stacheldraht um die badewanne? .. damit sie nicht so weit raus schwimmen", "kommt ein reporter zu einem bauern, dieser heißt fritz. fragt der reporter, fritz wo sind deine großeltern? darauf fritz: vom träcker überfahren und wo sind deine eltern? auch vom träcker überfahren. ja aber deine geschwister? alle vom träcker über fahren. Darauf fragt der reporter: ja fritz aber was machst du denn nun den ganzen tag? fritz: Träcker faahrn", "Kommt ein Junge zum Arzt und sagt: hallo, ich bin kevin und habe ein großes problem.... da kann ich ihnen leider nicht helfen kevin hahaha", "Die Dame zum angegrauten Straßenbahnschaffner: Verkehren Sie viertelstündlich?. Aber gnädige Frau, in meinem Alter?", "Lebensweisheit: Hart ist hart und weich ist weich, aber immer weich ist ganz schön hart!", "Chef zum Mitarbeiter: Haben Sie Ihre Frau betrogen? Wer denn sonst?", "der ehemann erwischt seine frau inflagranti und schreit was macht ihr denn da. die frau zum liebhaber siehst du ich hab es dir ja gesagt davon versteht er auch nichts", "alte hamburger oma zum polizisten was sind das für mädchen dahinten die da mit den kurzen röcken und den handtaschen am arm? Prostituierte gnädige Frau. Wenn da man nicht auch ein paar Nutten drunter sind!" };
            String[] diktieren = new string[2] { "rede bitte deutlich mein freund", "diktierfunktion wird gestartet" };
            String[] diktierencls = new string[2] { "ich hoffe es ist alles richtig geschrieben", "schliesse diktierfunktion" };
            String[] aktivieren = new string[4] { "endlich wieder reden", "ich höre zu", "waskann ich für dich tun ", "lass uns reden" };
            String[] ausruhen = new string[2] { "bye bye", "bis bald mein freund" };
            String[] speechinfo = new string[4] { "ich soll dir etwas über mich erzählen? ok!", "gebe informationen über meine funktionen aus", "na aber sicher doch", "ich dachte du kennst mich, nagut weil du es bist" };

            string modus = e.Result.Text;
            if (modus == "aktivieren")
            {
                hören = true;
            }
            if (modus == "ruhemodus")
            {
                hören = false;
            }
            if (Suche)
            {
                Process.Start("https://www.google.de/search?num=100&hl=de&source=hp&ei=Htr6Wv3OL6q56ASazLnIBw&q=" + modus);
                Suche = false;
            }
            if (Wasist)
            {
                try
                {
                    speech.SpeakAsync("ich habe dazu folgendes gefunden:");
                    using (Stream stream = client.OpenRead("http://de.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&explaintext=1&titles=" + modus))
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        Result result = json.Deserialize<Result>(new JsonTextReader(reader));
                        foreach (Page page in result.Query.Pages.Values)
                            Txt_info.Text = page.Extract;
                        string antwort = Txt_info.Text;
                        string small = antwort.Substring(0, 500);
                        speech.SpeakAsync(small);
                    }
                    Wasist = false;
                }
                catch (Exception)
                {
                    speech.SpeakAsync("ich habe diesbezüglich keinen eintrag finden können");
                    Wasist = false;
                }
            }
            if (hören == true && Suche == false && Wasist == false)
            {
                Txt_info.Text = e.Result.Text;
                switch (e.Result.Text)
                {
                    case "definiere":
                        Wasist = true;
                        break;

                    case "aktivieren":
                        speech.SpeakAsync(aktivieren[r.Next(4)]);
                        engine.RecognizeAsync();
                        hören = true;
                        break;

                    case "ruhemodus":
                        hören = false;
                        engine.RecognizeAsyncStop();
                        break;

                    case "guck guck":
                    case "hallo junitt":
                        speech.SpeakAsync(hallostefan[r.Next(7)]);
                        break;

                    case "wie heißt du":
                    case "wer bist du":
                        speech.SpeakAsync(wer[r.Next(4)]);
                        break;

                    case "was bist du":
                        speech.SpeakAsync(was[r.Next(4)]);
                        break;

                    case "welches datum haben wir":
                    case "welcher tag ist heute":
                        speech.SpeakAsync(DateTime.Now.ToString("d"));
                        break;

                    case "nenne mir die uhrzeit":
                    case "wie spät ist es":
                        speech.SpeakAsync(DateTime.Now.ToString("HH:mm:ss tt"));
                        break;

                    case "was machen wir heute":
                        speech.SpeakAsync(pinkyundbrain[r.Next(4)]);
                        break;

                    case "vielen dank":
                    case "danke junitt":
                        speech.SpeakAsync(danke[r.Next(4)]);
                        break;

                    case "machs gut junitt":
                    case "bis bald":
                        speech.SpeakAsync(bisbald[r.Next(4)]);
                        break;

                    case "wie ist dein befinden":
                    case "wie geht es dir":
                        speech.SpeakAsync(befinden[r.Next(6)]);
                        break;

                    case "junitt bring mich zum lachen":
                    case "kennst du witze":
                        speech.SpeakAsync(witzig[r.Next(5)]);
                        break;

                    case "maximieren":
                        speech.SpeakAsync(maximieren[r.Next(4)]);
                        WindowState = FormWindowState.Normal;
                        break;

                    case "minimieren":
                        speech.SpeakAsync(minimieren[r.Next(4)]);
                        WindowState = FormWindowState.Minimized;
                        break;

                    case "starte registrierung":
                        speech.SpeakAsync(registrierung[r.Next(4)]);
                        Process.Start(fileName: "regedit");
                        break;

                    case "registrierung schließen":
                        foreach (var process in Process.GetProcessesByName("regedit"))
                        {
                            process.Kill();
                        }
                        break;

                    case "welche windows version habe ich":
                        speech.SpeakAsync(windowsver1[r.Next(3)]);
                        Process.Start(fileName: "winver");
                        break;

                    case "fenster schliessen":
                        foreach (var process in Process.GetProcessesByName("winver"))
                        {
                            process.Kill();
                        }
                        break;

                    case "starte paint":
                    case "lass uns zeichnen":
                        speech.SpeakAsync(zeichnen[r.Next(4)]);
                        Process.Start(fileName: "mspaint");
                        break;

                    case "beende paint":
                        speech.SpeakAsync(zeichnencls[r.Next(4)]);
                        foreach (Process process in Process.GetProcessesByName("mspaint"))
                        {
                            process.Kill();
                        }
                        break;

                    case "starte browser":
                    case "internet":
                        speech.SpeakAsync(internet[r.Next(4)]);
                        Process.Start(fileName: "firefox");
                        break;

                    case "beende browser":
                        speech.SpeakAsync(internetcls[r.Next(4)]);
                        foreach (Process process in Process.GetProcessesByName("firefox"))
                        {
                            process.Kill();
                        }
                        break;

                    case "starte editor":
                        speech.SpeakAsync(anwendung[r.Next(4)]);
                        Process.Start("notepad");
                        break;

                    case "editor beenden":
                        foreach (Process process in Process.GetProcessesByName("notepad"))
                        {
                            process.Kill();
                        }
                        break;

                    case "abbrechen":
                        speech.SpeakAsyncCancelAll();
                        speech.SpeakAsync(abbrechen[r.Next(4)]);
                        break;

                    case "pause":
                        speech.Pause();
                        break;

                    case "weiter":
                        speech.Resume();
                        break;

                    case "junitt, geh schlafen":
                    case "ruh dich mal aus":
                        speech.SpeakAsync(ausruhen[r.Next(2)]);
                        engine.UnloadAllGrammars();
                        Close();
                        break;

                    case "stoppe sprachausgabe":
                    case "still junitt":
                        speech.SpeakAsyncCancelAll();
                        break;

                    case "infos aus diesem verzeichnis":
                        string pfad = Convert.ToString(webBrowser1.Url);
                        pfad = pfad.Substring(8);
                        var anzahlordner = Directory.GetDirectories(pfad).Length;
                        var anzahldaten = Directory.GetFiles(pfad).Length;
                        speech.SpeakAsync("In dem momentan ausgewähltem verzeichnis" + webBrowser1.DocumentTitle +
                        "befinden sich" + anzahlordner + "ordner und" + anzahldaten + "dateien");
                        break;

                    case "welche ordner sind in diesem verzeichnis":
                        string pfad2 = Convert.ToString(webBrowser1.Url);
                        pfad = pfad2.Substring(8);
                        string[] ordnernamen = Directory.GetDirectories(pfad);
                        foreach (string name in ordnernamen)
                        {
                            FileInfo f = new FileInfo(name);
                            Txt_info.Text = (f.Name + "\n");
                        }
                        speech.SpeakAsync("In deinem verzeichnis existieren folgende ordner" + Txt_info.Text);
                        break;

                    case "wie ist das wetter":
                        GetWeather();
                        break;

                    case "finde":
                        Suche = true;
                        break;

                    case "lies mir meine ablage vor":
                        String zwischenablage = null;
                        if (Clipboard.ContainsText())
                        {
                            zwischenablage = Clipboard.GetText();
                            Txt_info.Text = zwischenablage;
                            speech.SpeakAsync(zwischenablage);
                        }
                        break;

                    case "gib mir infos deiner funktionen":
                        speech.SpeakAsync(speechinfo[r.Next(4)]);
                        speech.SpeakAsync("wenn du sprichst erhalte ich" + engine.AudioFormat);
                        speech.SpeakAsync("im moment höre ich dich mit einer lautstärke von" + engine.AudioLevel);
                        speech.SpeakAsync("Mehr Infos über mich findest du unter" + engine.RecognizerInfo);
                        speech.SpeakAsync("Die maximale anzahl von erkennungsergebnissen für eine operation ist" + engine.MaxAlternates);
                        speech.SpeakAsync(textToSpeak: "Momentan bin ich auf" + engine.AudioState + "geschaltet");
                        break;

                    case "TTS Anwendung starten":
                        Launch(new TTSForm());
                        break;

                    case "TTS beenden":
                        engine.UnloadAllGrammars();
                        Close();
                        WindowState = FormWindowState.Normal;
                        break;

                }
            }
        }
        async void GetWeather()
        {
            var client = new OpenWeatherMapClient("ab3048efd457820d58044f2dc14b7d5c");
            var currentWeather = await client.CurrentWeather.GetByName("Berlin");
            int Temperatur = Convert.ToInt16(currentWeather.Temperature.Value - 273);
            speech.SpeakAsync("der himmel ist:" + currentWeather.Weather.Value);
            Lbl_weather.Text = currentWeather.Weather.Value;
            speech.SpeakAsync("und die aktuelle Temperatur beträgt:" + Temperatur + "Grad Celsius");
            Lbl_weather.Text = Temperatur + " Grad Celsius";
        }


        private void Timer1_Tick(object sender, EventArgs e)
        {
            LbL_CPU.Text = "CPU Auslastung: " + (int)perfCPU.NextValue() + "%";
            Lbl_RAM.Text = "Freier Speicher: " + (int)perfRAM.NextValue() + " Megabyte";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Show();
            pictureBox2.Hide(); ;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Show();
            pictureBox1.Hide();
        }

        private void Form1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch(new TTSForm());
        }

        private void Form2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch(new DeltaRuleForm());
        }

        private void Form3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch(new Form3());
        }

        private void Form4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Launch(new Form4());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void Launch(Form form)
        {
            form.Show();
            form.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            WindowState = FormWindowState.Minimized;
        }

        void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.UnloadAllGrammars();
            Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            engine.UnloadAllGrammars();
            Close();
        }
    }

    public class Result
    {
        public Query Query { get; set; }
    }
    public class Query
    {
        public Dictionary<string, Page> Pages { get; set; }
    }
    public class Page
    {
        public string Extract { get; set; }
    }
}