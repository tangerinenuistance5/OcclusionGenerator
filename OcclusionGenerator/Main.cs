using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OcclusionGenerator {
    public partial class Main : Form {
        XDocument XDocItyp;
        XDocument XDocImap;

        CommonOpenFileDialog filePicker = new CommonOpenFileDialog
        {
            EnsurePathExists = true,
            IsFolderPicker = false,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "Select File",
        };

        CommonOpenFileDialog folderPicker = new CommonOpenFileDialog
        {
            EnsurePathExists = true,
            IsFolderPicker = true,
            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Title = "Select Folder to Save File",
        };
        public Main() {
            InitializeComponent();
            filePicker.Filters.Add(new CommonFileDialogFilter("XML Game Data", "*.xml"));

        }

        private void LogAction(string msg, bool newLine = true) {
            if (newLine) {
                if (actionsLog.Text != "")
                    actionsLog.AppendText("\r\n");
            }
            actionsLog.AppendText(msg);
        }

        private void Main_Load(object sender, EventArgs e) {

        }

        private void ityp_Select_Click(object sender, EventArgs e) {

            CommonFileDialogResult dialogResult = filePicker.ShowDialog();
            if (dialogResult == CommonFileDialogResult.Ok) {
                string directory = filePicker.FileName;
                itypSelectionBox.Text = directory;

                XDocItyp = XDocument.Load(directory);
            }
        }

        private void imap_Select_Click(object sender, EventArgs e) {
            CommonFileDialogResult dialogResult = filePicker.ShowDialog();
            if (dialogResult == CommonFileDialogResult.Ok) {
                string directory = filePicker.FileName;
                imapSelectionBox.Text = directory;

                XDocImap = XDocument.Load(directory);
            }
        }

        private void generateAudioOcclusion_Click(object sender, EventArgs e) {

            if (XDocImap != null && XDocItyp != null) {
                CommonFileDialogResult dialogResult = folderPicker.ShowDialog();

                if (dialogResult == CommonFileDialogResult.Ok) {
                    string directory = folderPicker.FileName;
                    MloInterior mloInterior = ParseXml.GetMloInterior(XDocItyp, XDocImap);
                    PortalInfoList portalInfoList = naOcclusionInteriorMetadata.GetPortalInfoList(mloInterior);
                    PathNodeList pathNodeList = naOcclusionInteriorMetadata.GetPathNodeList(portalInfoList, mloInterior);

                    if (mloInterior.name.StartsWith("hash_") || mloInterior.Rooms.Any(room => room.name.StartsWith("hash_"))) {
                        MessageBox.Show("CMloArchetypeDef, CMloInstanceDef, or CMloRoomDef had a hashed name! Real strings are required.", "Occlusion Generator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    LogAction(">> Generated " + mloInterior.occlHash.occlusionHash + ".ymt.pso.xml");

                    naOcclusionInteriorMetadata.SavePsoXML(directory, mloInterior.occlHash.occlusionHash, portalInfoList, pathNodeList);
                }
            } else {
                MessageBox.Show("Empty field!", "Occlusion Generator", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void actionsLog_TextChanged(object sender, EventArgs e) {

        }
    }
}
