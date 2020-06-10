using System;

namespace Server.Items
{
    public class PumpkinTallSpirit : BaseLight
    {
        [Constructable]
        public PumpkinTallSpirit()
            : base(0x994D)
        {
            //this.Name = "Tall Pumpkin Spirit";
            if (Burnout)
                this.Duration = TimeSpan.FromMinutes(20);
            else
                this.Duration = TimeSpan.Zero;

            this.Burning = false;
            this.Light = LightType.Circle150;
            this.Weight = 1.0;
        }

        public PumpkinTallSpirit(Serial serial)
            : base(serial)
        {
        }

        public override int LabelNumber
        {
            get
            {
                return 1096943;
            }
        }// carved pumpkin

        public override int LitItemID
        {
            get
            {
                return 0x994E;
            }
        }
        public override int UnlitItemID
        {
            get
            {
                return 0x994D;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}