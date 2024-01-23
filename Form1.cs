using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace poscon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\tlocal desiredParts = {}");
            MatchCollection models = Regex.Matches(textBox1.Text, @"\[""Model""\]\s=\s""(.*?)""");
            MatchCollection positions = Regex.Matches(textBox1.Text, @"\[""Position""\]\s=\sVector\((.*?)\)");
            MatchCollection angles = Regex.Matches(textBox1.Text, @"\[""Angles""\]\s=\sAngle\((.*?)\)");
            MatchCollection materials = Regex.Matches(textBox1.Text, @"\[""Material""\]\s=\s""(.*?)""");
            MatchCollection colors = Regex.Matches(textBox1.Text, @"\[""Color""\]\s=\s(Vector\(.*?\))");
            for (int i = 0; i < models.Count; i++)
            {
                sb.AppendLine(string.Format("\tdesiredParts[{0}]", i) + " = {}");
                sb.AppendLine(string.Format("\tdesiredParts[{0}]", i) + "[\"model\"] = \"" + models[i].Groups[1].Value + "\"" );
                sb.AppendLine(string.Format("\tdesiredParts[{0}]", i) + "[\"pos\"] = Vector(" + positions[i].Groups[1].Value + ")" );
                sb.AppendLine(string.Format("\tdesiredParts[{0}]", i) + "[\"ang\"] = Angle(" + angles [i].Groups[1].Value + ")");
                sb.AppendLine(string.Format("\tdesiredParts[{0}][\"bone\"]", i) + " = \"ValveBiped.Bip01_Spine4\"");
                sb.AppendLine(string.Format("\tdesiredParts[{0}][\"mat\"] = \"{1}\"", i, materials[i].Groups[1].Value ));
                sb.AppendLine(string.Format("\tdesiredParts[{0}][\"col\"] = {1}", i, colors[i].Groups[1].Value));
            }

            string code =Properties.Resources.mainscript.Replace("replace_me", sb.ToString() + "\r\n\t" +  Properties.Resources.secondpart );

            textBox2.Text = code;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }
    }
}
