﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
{
    public class BoxFillProfileWidget : ProfileWidget
    {
        private int width = 150;
        private int height = 150;

        public BoxFillProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
            : base(parent, row, particleEmitter)
        {
            var profile = Profile.BoxFill(width, height);
            particleEmitter.Profile = profile;

            var grid = buildGrid(parent, row, 3, 1);

            var widthButton = GUI.createSpinButton(grid, "Width", 0);
            var heigthButton = GUI.createSpinButton(grid, "Height", 1);
            widthButton.Value = width;
            heigthButton.Value = height;

            heigthButton.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                {
                    height = (int)s.NewValue;
                    particleEmitter.Profile = Profile.BoxFill((int)s.NewValue, height);
                }
            };
            widthButton.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                {
                    height = (int)s.NewValue;
                    particleEmitter.Profile = Profile.BoxFill(width, (int)s.NewValue);
                }
            };
        }

        public BoxFillProfileWidget()
            : base(null, 0, null)
        {

        }

        public override ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new BoxFillProfileWidget(parent, row, particleEmitter);
        }
    }
}
