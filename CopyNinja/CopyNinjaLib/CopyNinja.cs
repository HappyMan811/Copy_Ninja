using Roniz.WCF.P2P.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyNinjaLib
{
    public class CopyNinja
    {
        SynchronizationStateManager stateManager;

        ClipboardP2P clipboard;

        /// <summary>
        /// 
        /// </summary>
        public CopyNinja()
        {
            clipboard = new ClipboardP2P();

            stateManager = new SynchronizationStateManager(clipboard);            
        }


        public void Run()
        {
            stateManager.Open();
        }

        public void Stop()
        {
            stateManager.Close();
        }
        
    }
}
