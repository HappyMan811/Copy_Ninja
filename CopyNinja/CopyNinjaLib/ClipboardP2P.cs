using Roniz.WCF.P2P.Messages.Presence;
using Roniz.WCF.P2P.Sync;
using Roniz.WCF.P2P.Sync.Enums;
using Roniz.WCF.P2P.Sync.Interfaces;
using Roniz.WCF.P2P.Sync.Messages;
using Roniz.WCF.P2P.Sync.Messages.BusinessLogic;
using System.Linq;
using System.Net.NetworkInformation;

namespace CopyNinjaLib
{
    public class ClipboardP2P : ISynchronizationBusinessLogic
    {
        public bool IsNeedFullSynchronization { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        //------------------------------------------------------------------------//

        public ClipboardP2P()
        {

        }


        public FullPresenceInfo ProvideFullPresenceInfo()
        {
            throw new System.NotImplementedException();
        }

        public void OnOnlineAnnouncementReceived(FullPresenceInfo fullPresenceInfo)
        {
            throw new System.NotImplementedException();
        }

        public CompactPresenceInfo ProvideCompactPresenceInfo()
        {
            throw new System.NotImplementedException();
        }

        public void OnOfflineAnnouncementReceived(CompactPresenceInfo compactPresenceInfo)
        {
            throw new System.NotImplementedException();
        }

        public void OnPresenceInfoChangedReceived(FullPresenceInfo fullPresenceInfo)
        {
            throw new System.NotImplementedException();
        }

        public BusinessLogicMessageBase ProvideFullSynchronizationDetailResponse()
        {
            throw new System.NotImplementedException();
        }

        public void OnCommunicationStateChanged(SynchronizationCommunicationState oldState, SynchronizationCommunicationState newState)
        {
            throw new System.NotImplementedException();
        }

        public void OnSynchronizationDetailsResponseReceived(BusinessLogicMessageBase synchronizationDetailsResponse)
        {
            throw new System.NotImplementedException();
        }

        public void OnUpdateReceived(BusinessLogicMessageBase stateMessage)
        {
            throw new System.NotImplementedException();
        }

        public BusinessLogicMessageBase ProvideSynchronizationDetailRequest(BusinessLogicMessageBase synchronizationResponse)
        {
            throw new System.NotImplementedException();
        }

        public BusinessLogicMessageBase ProvideSynchronizationDetailResponse(BusinessLogicMessageBase synchronizationDetailsRequest)
        {
            throw new System.NotImplementedException();
        }

        public BusinessLogicMessageBase ProvideSynchronizationResponse(SynchronizationRequest synchronizationRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}
