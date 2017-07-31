using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISoundAlarm
    {
        /// <summary>
        /// Spielt fehlerton ab
        /// </summary>
        void Alarm();
        void Alarm(float volume);

        /// <summary>
        /// Spielt Benachrichtigungston ab
        /// </summary>
        void Info();

        /// <summary>
        /// Spielt Warnungston ab
        /// </summary>
        void Notify();
    }
}
