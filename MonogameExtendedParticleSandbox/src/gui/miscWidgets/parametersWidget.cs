using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended;
using MonoGame.Extended.Particles;
using MonoGame.Extended.Particles.Profiles;
using Myra.Graphics2D.UI;

namespace MonogameExtendedParticleSandbox.src.gui.miscWidgets
{
    public class parametersWidget
    {

        private readonly string CAPACITY = "Capacity";
        private readonly string LIFE_SPAN = "Life Span (ms)";
        private readonly string SPEED = "Speed";
        private readonly string QUANTITY = "Quantity";
        private readonly string ROTATION = "Rotation";
        private readonly string SCALE = "Scale";

        public EmitterIndex index { get; }
        private ParticleController controller;
        public Grid grid { get; set; }
        private GridSizeHolder gridSizeHolder = new GridSizeHolder();
        public Grid topLevelGrid { get; }

        private CheckBox autoTriggerCheckbox;
        private SpinButton autoTriggerFrequency;
        private ComboBox strategy;

        public readonly int DEFAULT_QUANTITY = 300;
        public readonly int DEFAULT_TIMESPAN = 2000;
        public readonly int DEFAULT_CAPCITY = 300;
        public readonly int DEFAULT_ROTATION_1 = -1;
        public readonly int DEFAULT_ROTATION_2 = 1;
        public readonly int DEFAULT_SPEED_1 = 1;
        public readonly int DEFAULT_SPEED_2 = 50;
        public readonly int DEFAULT_SCALE_1 = 3;
        public readonly int DEFAULT_SCALE_2 = 4;

        public SpinButton c, ls, quantity, speed1, speed2, rotation1, rotation2, scale1, scale2;

        private Dictionary<string, ParticleModifierExecutionStrategy> strategies =
            new Dictionary<string, ParticleModifierExecutionStrategy>()
            {
                { "Parallel", ParticleModifierExecutionStrategy.Parallel },
                { "Serial", ParticleModifierExecutionStrategy.Serial },
            };

        public parametersWidget(ParticleController controller, GridSizeHolder topLevelGridRowNum, Profile profile, Grid topLevelGrid, int rows, int columns)
        {
            this.controller = controller;

            grid = new Grid
            {
                ColumnSpacing = 8,
                GridRow = topLevelGridRowNum.RowCount
            };
            this.topLevelGrid = topLevelGrid;
            for (int i = 0; i < columns; i++)
                grid.ColumnsProportions.Add(new Proportion());
            for (int i = 0; i < rows; i++)
                grid.RowsProportions.Add(new Proportion());
            topLevelGrid.AddChild(grid);

            c = GUI.createSpinButton(grid, CAPACITY, gridSizeHolder.RowCount++);
            ls = GUI.createSpinButton(grid, LIFE_SPAN, gridSizeHolder.RowCount++);
            quantity = GUI.createSpinButton(grid, QUANTITY, gridSizeHolder.RowCount++);

            speed1 = GUI.createSpinButton(grid, SPEED, gridSizeHolder.RowCount);
            speed2 = GUI.createSpinButton(grid, SPEED, gridSizeHolder.RowCount++, 2, false);

            rotation1 = GUI.createSpinButton(grid, ROTATION, gridSizeHolder.RowCount);
            rotation2 = GUI.createSpinButton(grid, ROTATION, gridSizeHolder.RowCount++, 2, false);

            scale1 = GUI.createSpinButton(grid, SCALE, gridSizeHolder.RowCount);
            scale2 = GUI.createSpinButton(grid, SCALE, gridSizeHolder.RowCount++, 2, false);

            index = controller.addEmitter(new ParticleEmitter(controller.getRegion(), DEFAULT_CAPCITY, TimeSpan.FromMilliseconds(DEFAULT_TIMESPAN),
                profile)
            {
                Parameters = new ParticleReleaseParameters
                {
                    Speed = new Range<float>(DEFAULT_SPEED_1, DEFAULT_SPEED_2),
                    Quantity = DEFAULT_QUANTITY,
                    Rotation = new Range<float>(DEFAULT_ROTATION_1, DEFAULT_ROTATION_2),
                    Scale = new Range<float>(DEFAULT_SCALE_1, DEFAULT_SCALE_2)
                }
            });

            quantity.Value = DEFAULT_QUANTITY;
            rotation1.Value = DEFAULT_ROTATION_1;
            rotation2.Value = DEFAULT_ROTATION_2;


            scale1.Value = DEFAULT_SCALE_1;
            scale2.Value = DEFAULT_SCALE_2;

            speed1.Value = DEFAULT_SPEED_1;
            speed2.Value = DEFAULT_SPEED_2;
            c.Value = DEFAULT_CAPCITY;
            ls.Value = DEFAULT_TIMESPAN;
            quantity.Value = DEFAULT_QUANTITY;

            autoTriggerCheckbox = GUI.createCheckBox(grid, "Auto Trigger: ", gridSizeHolder.RowCount++);
            autoTriggerCheckbox.PressedChanged += (s, e) =>
            {
                controller.getEmitter(index).AutoTrigger = autoTriggerCheckbox.IsChecked;
            };
            controller.getEmitter(index).AutoTrigger = autoTriggerCheckbox.IsChecked = true;

            var autoTriggerFrequency =
                GUI.createSpinButton(grid, "Auto Trigger Frequency: ", gridSizeHolder.RowCount++);
            autoTriggerFrequency.ValueChanged += (s, e) =>
            {
                if (autoTriggerFrequency.Value != null)
                    controller.getEmitter(index).AutoTriggerFrequency = (float)autoTriggerFrequency.Value;
            };
            autoTriggerFrequency.Value = controller.getEmitter(index).AutoTriggerFrequency;

            Label stratLabel = new Label()
            {
                GridRow = gridSizeHolder.RowCount,
                GridColumn = 0,
                Text = "Execution Strategy"
            };
            grid.AddChild(stratLabel);
            strategy = GUI.createComboBox(grid, gridSizeHolder.RowCount++, 1,
                GUI.convertDictionaryToList(strategies),
                (i, box) =>
                {
                    if (strategy != null)
                        controller.getEmitter(index).ModifierExecutionStrategy = strategies[strategy.Items[i].Text];
                    Console.Out.WriteLine(controller.getEmitter(index).ModifierExecutionStrategy.ToString());
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
                controller.getEmitter(index).Parameters.Rotation = calculateNewRangeMin(
                    controller.getEmitter(index).Parameters.Rotation,
                    v,
                    rotation2);
            });
            rotation2.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Rotation = calculateNewRangeMax(
                    controller.getEmitter(index).Parameters.Rotation,
                    v,
                    rotation1);
            });
            scale1.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Scale = calculateNewRangeMin(
                    controller.getEmitter(index).Parameters.Scale,
                    v,
                    scale2);
            });
            scale2.ValueChanged += GUI.createFloatEventHandler((v) =>
            {
                controller.getEmitter(index).Parameters.Scale = calculateNewRangeMax(
                    controller.getEmitter(index).Parameters.Scale,
                    v,
                    scale1);
            });

        }

        public void delete()
        {
            grid.RemoveChild(c);
            grid.RemoveChild(ls);
            grid.RemoveChild(quantity);
            grid.RemoveChild(speed1);
            grid.RemoveChild(speed2);
            grid.RemoveChild(rotation1);
            grid.RemoveChild(rotation1);
            grid.RemoveChild(scale1);
            grid.RemoveChild(scale2);
            topLevelGrid.RemoveChild(grid);
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
