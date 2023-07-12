using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueProtocolEcho
{
    internal class TextBoxWriter : TextWriter
    {
        private TextBox _outputBox = null;

        public TextBoxWriter(TextBox control)
        {
            _outputBox = control;
        }

        public override void Write(char value)
        {
            InvokeControl(_outputBox, x => x.AppendText(value.ToString()));
        }

        public override void Write(char[] buffer, int index, int count)
        {
            InvokeControl(_outputBox, x => x.AppendText(new string(buffer)));
        }

        public override void Write(string value)
        {
            InvokeControl(_outputBox, x => x.AppendText(value));
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        private void InvokeControl<T>(T Control, Action<T> Action) where T : Control
        {
            if (Control.InvokeRequired)
            {
                Control.Invoke(new Action<T, Action<T>>(InvokeControl), new object[] { Control, Action });
            }
            else
            {
                Action(Control);
            }
        }
    }
}
