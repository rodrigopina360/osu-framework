// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuTK;

namespace osu.Framework.Platform
{
    /// <summary>
    /// This class allows manipulation of the taskbar notification icon.
    /// </summary>
    public abstract class Taskbar
    {
        public abstract event EventHandler OnClickNotifyIconEvent;
        /// <summary>
        /// Create taskbar notification icon.
        /// </summary>
        public abstract bool CreateTaskbarIcon();
        /// <summary>
        /// Destroy taskbar notification icon.
        /// </summary>
        public abstract bool DestroyTaskbarIcon();
    }
}
