#define OFFLINE_SYNC_ENABLED


using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices;

namespace XamarinLearning.Helpers
{
    public class AzureAppServiceHelper
    {
        

        public static async Task SyncAsync()
        { 
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await App.MobileService.SyncContext.PushAsync();

                await App.postTable.PullAsync("userPosts", "");
            }
            catch (MobileServicePushFailedException mspfe)
            {
                if (mspfe.PushResult != null)
                {
                    syncErrors = mspfe.PushResult.Errors;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update &&
                        error.Result != null)
                    {
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        await error.CancelAndDiscardItemAsync();
                    }
                }
            }
        }
    }

}
