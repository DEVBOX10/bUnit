﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace Egil.RazorComponents.Testing.EventDispatchExtensions
{
    public class PointerEventDispatchExtensionsTest : EventDispatchExtensionsTest<PointerEventArgs>
    {
        protected override string ElementName => "div";

        [Theory(DisplayName = "Pointer events are raised correctly through helpers")]
        [MemberData(nameof(GetEventHelperMethods), typeof(PointerEventDispatchExtensions))]
        public async Task CanRaiseEvents(MethodInfo helper)
        {
            var expected = new PointerEventArgs()
            {
                // mouse specific
                Detail = 2,
                ScreenX = 3,
                ScreenY = 4,
                ClientX = 5,
                ClientY = 6,
                Button = 7,
                Buttons = 8,
                CtrlKey = true,
                AltKey = true,
                MetaKey = true,
                ShiftKey = true,
                Type = "TYPE",
                
                // pointer specific
                Height = 100F,
                Width = 101F,
                TiltX = 102F,
                TiltY = 103F,
                IsPrimary = true,
                PointerId = 4242L,
                Pressure = 1337F,
                PointerType = "MOUSE"
            };

            await VerifyEventRaisesCorrectly(helper, expected);
        }
    }
}
