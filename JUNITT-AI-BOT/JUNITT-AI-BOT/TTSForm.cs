using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace WindowsFormsApp14
{
    internal partial class TTSForm : MaterialForm
    {
        SpeechSynthesizer reader;
        SpeechRecognitionEngine engine = new SpeechRecognitionEngine();

        public TTSForm()
        {
            InitializeComponent();
            Timer1.Start();
            Choices choices = new Choices();
            DictationGrammar dictation = new DictationGrammar();
            GrammarBuilder grammarBuilder = new GrammarBuilder(choices);
            grammarBuilder.AppendDictation();
            engine.LoadGrammar(dictation);
            engine.SetInputToDefaultAudioDevice();
            engine.RecognizeAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            reader = new SpeechSynthesizer();
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox1.ScrollBars = ScrollBars.Both;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            reader.Dispose();
            if (textBox1.Text != "")
            {
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(textBox1.Text);
                label12.Text = "SPRECHE";

                button2.Enabled = true;
                button4.Enabled = true;
                reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(Reader_SpeakCompleted);
            }
            else
            {
                textBox1.AppendText("Text in das felds schreiben bitte" + Environment.NewLine + "Nachricht" + Environment.NewLine + MessageBoxButtons.OK);
            }
            textBox1.Clear();

        }
        void Reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            label12.Text = "LEERLAUF";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Speaking)
                {
                    reader.Pause();
                    label12.Text = "PAUSE";
                    button3.Enabled = true;

                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                    label12.Text = "SPRECHE";
                }
                button3.Enabled = false;
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                reader.Dispose();
                label12.Text = "LEERLAUF";
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void OpenFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName.ToString());
        }
    }
}
