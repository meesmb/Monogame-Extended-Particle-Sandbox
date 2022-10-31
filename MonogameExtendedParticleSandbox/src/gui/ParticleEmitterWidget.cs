using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Modifiers;
using MonoGame.Extended.Particles.Profiles;
using MonogameExtendedParticleSandbox.src.gui.modifiers;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui
{
    public class ParticleEmitterWidget
    {

        private ParticleController controller;
        private int index = 0;

        private readonly string CAPACITY = "Capacity";
        private readonly string LIFE_SPAN = "Life Span (ms)";
        private readonly string SPEED = "Speed";
        private readonly string QUANTITY= "Quantity";
        private readonly string ROTATION = "Rotation";
        private readonly string SCALE = "Scale";

        private readonly int columns = 4;
        private readonly int rows = 20;

        private int topLevelGridRowNum = 0;

        private List<ModifierWidget> modifierWidgets = new List<ModifierWidget>();

        private Dictionary<string, ModifierWidget> widgetTypes = new Dictionary<string, ModifierWidget>()
        {
            {"Age", new AgeModifierWidget()}, // needs interpolators
            {"Circle", new CircleContainerModifierWidget()},
            {"RectangleLoop", new RectangleLoopContainerModifierWidget()},
            {"Drag", new DragModifierWidget()},
            {"Linear Gravity", new LinearGravityModifierWidget()},
            {"Opacity Fast Fade", new OpacityFastFadeModifierWidget()},
            {"Rotation Rate", new RotationModifierWidget()},
            {"Velocity Color", new VelocityColorModifierWidget()}, // needs color selector (HSL)
            {"Velocity", new VelocityModifierWidget()}, // needs color selector (HSL) and interpolators
            {"Vortex", new VortexModifierWidget()},

        };
        private string currentWidgetType = "Age";


        public ParticleEmitterWidget(ParticleController controller, ScrollViewer parent, Profile profile)
        {
            this.controller = controller;

            var topLevelGrid = new Grid()
            {
            };
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            topLevelGrid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                topLevelGrid.RowsProportions.Add(new Proportion());

            parent.Content = topLevelGrid;

            var grid = new Grid
            {
                ShowGridLines = false,
                ColumnSpacing = 8,
                GridRow = topLevelGridRowNum++
            };
            for (int i = 0; i < columns; i++) 
                grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                grid.RowsProportions.Add(new Proportion());
            topLevelGrid.AddChild(grid);

            var c = GUI.createSpinButton(grid, CAPACITY, 0);
            var ls = GUI.createSpinButton(grid, LIFE_SPAN, 1);
            var quantity = GUI.createSpinButton(grid, QUANTITY, 2);

            var speed1 = GUI.createSpinButton(grid, SPEED, 3);
            var speed2 = GUI.createSpinButton(grid, SPEED, 3, 2, false);

            var rotation1 = GUI.createSpinButton(grid, ROTATION, 4);
            var rotation2 = GUI.createSpinButton(grid, ROTATION, 4, 2, false);

            var scale1 = GUI.createSpinButton(grid, SCALE, 5);
            var scale2 = GUI.createSpinButton(grid, SCALE, 5, 2, false);

            index = controller.addEmitter(new ParticleEmitter(controller.getRegion(), 0, TimeSpan.FromMilliseconds(0),
                profile)
            {
                Parameters = new ParticleReleaseParameters
                {
                    Speed = new Range<float>(0f, 50f),
                    Quantity = 3,
                    Rotation = new Range<float>(-1f, 1f),
                    Scale = new Range<float>(3.0f, 4.0f)
                }
            });

            // parameters 
            c.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Capacity = (int)v;
            });
            ls.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).LifeSpan = TimeSpan.FromMilliseconds(v);
            });
            quantity.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Quantity = (int)v;
            });
            speed1.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMin(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    speed2);
            });
            speed2.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMax(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    speed1);
            });
            rotation1.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMin(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    rotation2);
            });
            rotation2.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMax(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    rotation1);
            });
            scale1.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMin(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    scale2);
            });
            scale2.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Speed = calculateNewRangeMax(
                    controller.getEmitter(index).Parameters.Speed,
                    v,
                    scale1);
            });


            // modifiers
            TextButton textButton = new TextButton()
            {
                Text = "New Modifier",
                GridRow = 6,
                GridColumn = 0
            };
            ComboBox combo = new ComboBox()
            {
                GridRow = 6,
                GridColumn = 1,
            };
            var items = createModifierWidgets();
            foreach (var item in items)
                combo.Items.Add(item);

            var modifierGrid = new Grid()
            {
                GridRow = 1,
                RowSpacing = 25
            };
            modifierGrid.ColumnsProportions.Add(new Proportion()
            {
                Type = ProportionType.Pixels,
                Value = 50
            });
            for (int i = 0; i < rows; i++)
                modifierGrid.RowsProportions.Add(new Proportion());
            topLevelGrid.AddChild(modifierGrid);

            var modifierGridRowNum = 0;
            combo.SelectedIndexChanged += (s, e) =>
            {
                if (combo.SelectedIndex != null)
                    currentWidgetType = combo.Items[(int)combo.SelectedIndex].Text;
            };
            textButton.Click += (s, e) =>
            {
                var deleteButton = new TextButton()
                {
                    Text = "-",
                    GridColumn = 0,
                    GridRow = modifierGridRowNum,
                };
                modifierGrid.AddChild(deleteButton);
                ++modifierGridRowNum;
                var modifier = widgetTypes[currentWidgetType].create(modifierGrid, modifierGridRowNum++, controller.getEmitter(index));
                
                deleteButton.Click += (s, e) =>
                {
                    modifierGrid.RemoveChild(deleteButton);

                    modifier.delete();
                    modifierWidgets.Remove(modifier);
                    modifierGridRowNum -= 2;
                };

                
                modifierWidgets.Add(modifier);
            };

            grid.Widgets.Add(combo);
            grid.Widgets.Add(textButton);
        }

        private List<ListItem> createModifierWidgets()
        {
            List<ListItem> items = new List<ListItem>();

            foreach (var widget in this.widgetTypes)
            {
                var item = new ListItem
                {
                    Text = widget.Key
                };
                items.Add(item);
            }

            return items;
        }

        private Range<float> calculateNewRangeMin(Range<float> old, float min, SpinButton two)
        {
            float max = old.Max;

            if (min >= max)
            {
                max = min;
                two.Value = max;
            }
            return new Range<float>(min, max);
        }
        private Range<float> calculateNewRangeMax(Range<float> old, float max, SpinButton one)
        {
            float min = old.Min;

            if (max <= min)
            {
                min = max;
                one.Value = min;
            }
            return new Range<float>(min, max);
        }


    }
}
