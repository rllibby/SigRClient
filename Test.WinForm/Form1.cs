using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpeakToMe.Core;
using SpeakToMe.Core.Interfaces;
using SpeakToMe.Presence;

namespace Test.WinForm
{
    public partial class Form1 : Form
    {
        public static string UserId = Guid.NewGuid().ToString("D");
        public static string ConversationId = Guid.NewGuid().ToString("D");
        private IPresence _Presence;

        public Form1()
        {
            InitializeComponent();

            BootStrapper bs = new BootStrapper();
            bs.Initialize();

            _Presence = ServiceLocator.GetInstance<TestPresence>();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;

                listBox1.Items.Add(textBox1.Text);

                _Presence.ProcessCommand(textBox1.Text, UserId, ConversationId, new CallbackWrapper(new Action<IErpResult>((msg) =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        if (msg == null)
                        {
                            listBox1.Items.Add(Constants.UnderstandFailure);
                        }
                        else if (msg.IsQuestion)
                        {
                            listBox1.Items.Add(msg.QuestionText);
                        }
                        else
                        {
                            listBox1.Items.Add(string.Format("Entity: {0}, Target: {1}, Context: \"{2}\"", msg.Entity, msg.Target, msg.Context));
                        }
                    }));
                })));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);

            _Presence.ProcessCommand(textBox1.Text, UserId, ConversationId, new CallbackWrapper(new Action<IErpResult>((msg) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (msg == null)
                    {
                        listBox1.Items.Add(Constants.UnderstandFailure);
                    }
                    else if (msg.IsQuestion)
                    {
                        listBox1.Items.Add(msg.QuestionText);
                    }
                    else
                    {
                        listBox1.Items.Add(string.Format("Entity: {0}, Target: {1}, Context: \"{2}\"", msg.Entity, msg.Target, msg.Context));
                    }
                }));
            })));
        }

        private void OnResponse(IErpResult result)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            long st = Environment.TickCount;

            for (int i = 0; i < 100000; i++)
            {
                _Presence.ProcessCommand("Give me the available quantity for xyz", UserId, ConversationId, new CallbackWrapper(this.OnResponse));
            }

            long et = Environment.TickCount - st;

            MessageBox.Show(string.Format("{0} ms to process 100,000 requests", et));
        }
    }
}
