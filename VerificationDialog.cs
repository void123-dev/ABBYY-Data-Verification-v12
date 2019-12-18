// © ABBYY. 2012.
// SAMPLES code is property of ABBYY, exclusive rights are reserved. 
// DEVELOPER is allowed to incorporate SAMPLES into his own APPLICATION and modify it 
// under the terms of License Agreement between ABBYY and DEVELOPER.

// Product: ABBYY FlexiCapture Engine 10

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using FCEngine;

namespace VisualEditor
{
    public partial class VerificationDialog : Form
    {      
        private IDocumentsCollection documents;
        
        public VerificationDialog( IEngine engine, IDocumentsCollection _documents )
        {
            InitializeComponent();

            // Initialize VerificationView component

            verificationView.Engine = engine;
            documents = _documents;
        }

        private void VerificationDialog_Load( object sender, EventArgs e )
        {
            try {
                // We will automatically start verification on load
                verificationView.StartVerification( documents );
            } catch( Exception ex ) {
                // This method will fail if verification is not allowed by license
                MessageBox.Show( ex.Message );
                Close();
            }
        }

        private void VerificationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (verificationView.IsVerifying)
            {
                //verificationView.SaveChanges();
            }
        }

        private void verificationView_VerificationCompleted( object sender, FCEngine.VisualComponents.VerificationCompletedEventArgs e )
        {
            Close();
        }

        private void cleanUp()
        {
            // This method is called from Dispose for proper cleanup. See VerificationDialog.Designer.cs

            // !!! Do not forget to propely clean up after verification.
            // During verification a lot of documents can be opened which need to be closed in timely manner.
            verificationView.Dispose();
        }
    }
}
