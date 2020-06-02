// SingleCall / Singleton / ClientAO 
#define Singleton

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;


namespace RemoteServer
{
    public partial class Form1 : Form, RemotableObjects.IObserver
    {
        private const int PORT = 8090;
        private const string APP_NAME = "RemoteTools";

        private enum EStatus
        {
            Unavailable,
            Connected,
            Disconnected
        }

        private readonly string[] STATUS_NAMES = { "Unavailable", "Connected", "Disconnected" };

        private bool _commandReady = false;
        private EStatus _status = EStatus.Unavailable;
        private bool _terminateReady = false;

        public Form1()
        {
            InitializeComponent();

            // using TCP protocol
            TcpChannel channel = new TcpChannel(PORT);
            ChannelServices.RegisterChannel(channel);

#if !(ClientAO)

            // Server Activated Objects
            RemotingConfiguration.RegisterWellKnownServiceType(
               typeof(RemotableObjects.RemoteClass),
               APP_NAME,
#if (SingleCall)
               WellKnownObjectMode.SingleCall);
#else
               WellKnownObjectMode.Singleton);
#endif

#else
            // Client Activated Objects
            RemotingConfiguration.ApplicationName = APP_NAME;
            RemotingConfiguration.RegisterActivatedServiceType(typeof(RemoteClass.RemoteClass));
#endif

            RemotableObjects.Cache.Attach(this);
        }

        public string NotifyInitAction(string text)
        {
            this._status = EStatus.Connected;
            this._commandReady = false;
            this._terminateReady = false;



            int iSpace = text.IndexOf(' ');
            if (iSpace == -1)
                return null;

            
            string address = text.Substring(0, iSpace);
            string info = text.Substring(iSpace + 1);

            this.textBoxAddress.Invoke((MethodInvoker)delegate
            {
                this.textBoxAddress.Text = address;
            });

            this.richTextBoxInfo.Invoke((MethodInvoker)delegate
            {
                this.richTextBoxInfo.Text = info;
            });

            this.textBoxStatus.Invoke((MethodInvoker)delegate
            {
                this.textBoxStatus.Text = STATUS_NAMES[(int)this._status];
            });

            this.textBoxCommand.Invoke((MethodInvoker)delegate
            {
                this.textBoxCommand.ReadOnly = false;
            });

            this.buttonExecute.Invoke((MethodInvoker)delegate
            {
                this.buttonExecute.Visible = true;
            });

            this.buttonTerminate.Invoke((MethodInvoker)delegate
            {
                this.buttonTerminate.Visible = true;
            });

            
            return string.Empty;
        }

        public string NotifyConnected(string text)
        {
            string command = string.Empty;
            if (!this._terminateReady)
            {
                if (this._commandReady == true && this._status == EStatus.Connected)
                {
                    this.textBoxCommand.Invoke((MethodInvoker)delegate
                    {
                        command = this.textBoxCommand.Text;
                        this.textBoxCommand.Text = string.Empty;

                        this.textBoxCommand.ReadOnly = false;                        
                        this.buttonExecute.Visible = true;

                        this._status = EStatus.Connected;
                        this._commandReady = false;
                        this._terminateReady = false;

                        this.textBoxStatus.Text = STATUS_NAMES[(int)this._status];
                    });
                }
            }
            else
                command = null;

            return command;
        }

        public string NotifyTerminate(string text)
        {
            this._status = EStatus.Disconnected;
            this._commandReady = false;
            this._terminateReady = false;


            this.textBoxCommand.Invoke((MethodInvoker)delegate
            {
                this.textBoxCommand.Text = string.Empty;
                this.textBoxCommand.ReadOnly = true;
                this.buttonExecute.Visible = false;
                this.buttonTerminate.Visible = false;
                this.textBoxStatus.Text = STATUS_NAMES[(int)this._status];
            });
        
           
            return string.Empty;
        }

        public string Notify(RemotableObjects.EAction action, string text)
        {
            if (action == RemotableObjects.EAction.Init)
            {
                return NotifyInitAction(text);
            }
            else if (action == RemotableObjects.EAction.Connected)
                return NotifyConnected(text);
            else
                return NotifyTerminate(text);
        }


        private void buttonExecute_Click(object sender, EventArgs e)
        {
            if (this.textBoxCommand.Text.Length > 0)
            {
                this.textBoxCommand.ReadOnly = true;
                this._commandReady = true;
                this.buttonExecute.Visible = false;
            }
        }

        private void buttonTerminate_Click(object sender, EventArgs e)
        {
            this.textBoxCommand.ReadOnly = true;
            this.buttonTerminate.Visible = false;
            this.buttonExecute.Visible = false;

            this._terminateReady = true;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }        
    }
}
