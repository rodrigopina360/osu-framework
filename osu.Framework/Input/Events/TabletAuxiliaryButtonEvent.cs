﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Extensions.TypeExtensions;
using osu.Framework.Input.States;

namespace osu.Framework.Input.Events
{
    /// <summary>
    /// Events of a tablet auxiliary button.
    /// </summary>
    public abstract class TabletAuxiliaryButtonEvent : TabletEvent
    {
        public readonly TabletAuxiliaryButton Button;

        protected TabletAuxiliaryButtonEvent(InputState state, TabletAuxiliaryButton button)
            : base(state)
        {
            Button = button;
        }

        public override string ToString() => $"{GetType().ReadableName()}({Button})";
    }
}
