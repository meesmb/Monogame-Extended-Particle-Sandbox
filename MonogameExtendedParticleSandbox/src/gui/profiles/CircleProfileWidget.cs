using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.profiles
{
    public class CircleProfileWidget : ProfileWidget
    {
        private int radiusValue = 150;
        private Profile.CircleRadiation radiation = Profile.CircleRadiation.In;

        private Dictionary<string, Profile.CircleRadiation> circleRadiations = new Dictionary<string, Profile.CircleRadiation>()
        {
            {"In", Profile.CircleRadiation.In},
            {"None", Profile.CircleRadiation.None},
            {"Out", Profile.CircleRadiation.Out},
        };

        public CircleProfileWidget(Grid parent, int row, ParticleEmitter particleEmitter)
        : base(parent, row, particleEmitter)
        {
            var profile = Profile.Circle(radiusValue, radiation);
            particleEmitter.Profile = profile;

            var grid = buildGrid(parent, row, 3, 1);

            var radius = GUI.createSpinButton(grid, "Radius", 0);
            radius.Value = radiusValue;

            radius.ValueChanged += (e, s) =>
            {
                if (s.NewValue != null)
                {
                    particleEmitter.Profile = Profile.Circle((int)s.NewValue, radiation);
                }
            };

            var label = new Label()
            {
                GridRow = 1,
                GridColumn = 0,
                Text = "Radiation: "
            };
            grid.AddChild(label);

            var combo = GUI.createComboBox(grid, 1, 1, GUI.convertDictionaryToList(circleRadiations), (v, ComboBox) =>
            {
                var radiation = circleRadiations[ComboBox.Items[v].Text];
                this.radiation = radiation;
                particleEmitter.Profile = Profile.Circle(radiusValue, radiation);
            }, 0);


        }

        public CircleProfileWidget()
            : base(null, 0, null)
        {

        }

        public override ProfileWidget create(Grid parent, int row, ParticleEmitter particleEmitter)
        {
            return new CircleProfileWidget(parent, row, particleEmitter);
        }
    }
}
