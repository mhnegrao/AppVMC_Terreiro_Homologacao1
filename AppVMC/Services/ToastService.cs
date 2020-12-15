using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace AppVMC.Services
{
    public class ToastService : IDisposable
    {
        public event Action<string, string, ToastLevel> OnShow;
        public event Action OnHide;
        private Timer Countdown;

        public void ShowToast(string heading, string message, ToastLevel level)
        {
            OnShow?.Invoke(heading, message, level);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();

            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new Timer(5000);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

#pragma warning disable CA1816 // Os métodos Dispose devem chamar SuppressFinalize
        public void Dispose()
#pragma warning restore CA1816 // Os métodos Dispose devem chamar SuppressFinalize
        {
            Countdown?.Dispose();
        }
    }
}
