#region AuthorHeader

//  
//	Runebook Library Changed by Partystuffcloseouts/Soultaker
//  Towns Ver. 1.0 
//  
//  Based on Runebook T-Hunting Files Original Ideas and code by // A_Li_N // Senior Member
//  
#endregion AuthorHeader
using System;
using System.IO;
using System.Collections;
using Server.Commands;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RuneLibraryTowns : Item
	{
		private static string pathlist = "Data/RuneLibraryTowns.txt";
		private static string entry = "Name Lastname X Y Z";
		private static string[] mapNames;
		private static string[] mapLastnames;
		private static string[] xs;
		private static string[] ys;
		private static string[] zs;
		private static int size = 0;

		private static ArrayList library;

      public static void Initialize()
      {
         CommandSystem.Register( "RuneLibraryTowns", AccessLevel.Administrator, new CommandEventHandler( RuneLibraryTowns_OnCommand ) );
      }

		public static void RuneLibraryTowns_OnCommand( CommandEventArgs args )
		{
			Mobile m = args.Mobile;
         	RuneLibraryTowns rl = new RuneLibraryTowns(m);
		}

		private static void readLine()
		{
			if( File.Exists( pathlist ) )
			{
				size = 0;
				string name = "";
				string lastname = "";
				string x = "";
				string y = "";
				string z = "";

				StreamReader f = new StreamReader( pathlist );
				while( (entry = f.ReadLine()) != null )
				{
					string[] parts = null;
					parts = entry.Split();

					name += parts[0]+" ";
					lastname += parts[1]+" ";
					x += parts[2]+" ";
					y += parts[3]+" ";
					z += parts[4]+" ";
					size++;
				}
				f.Close();

				mapNames = name.Split();
				mapLastnames = lastname.Split();
				xs = x.Split();
				ys = y.Split();
				zs = z.Split();
			}
		}

		[Constructable]
		public RuneLibraryTowns (Mobile from)
		{
			library = new ArrayList();

			readLine();
			Runebook rb = new Runebook(0);
			int nameStart = 1;
			int nameEnd = 1;
			for( int i=0; i<size; i++ )
			{
				if( rb.Entries.Count == 16 )
				{
					rb.Name = "Towns " + nameStart + " - " + (nameEnd-1);
					library.Add(rb);
					rb = new Runebook(0);
					nameStart = nameEnd;
				}
				int x = int.Parse(xs[i]);
				int y = int.Parse(ys[i]);
				int z = int.Parse(zs[i]);
				Point3D targ = new Point3D(x, y, z);
				RecallRune rr = new RecallRune();
				rr.Target = targ;
				rr.TargetMap = Map.Felucca;
				rr.Description = mapNames[i] + " " + mapLastnames[i];
				rr.House = null;
				rr.Marked = true;
				rb.OnDragDrop(from, rr );
				nameEnd++;
			}
			rb.Name = "Towns " + nameStart + " - " + (nameEnd-1);
			library.Add(rb);

			int height = 6;
			int offx;
			int offy;
			int offz;
			for(int p=0; p<library.Count; p++)
			{
				Runebook librarybook = (Runebook)library[p];
				librarybook.Movable = true;
				librarybook.MaxCharges = 10;
				librarybook.CurCharges = 10;				
				if(p < 4)
				{
					offx = from.Location.X-1;
					offy = from.Location.Y-1;
					offz = from.Location.Z+height;
				}
				else if(p >= 4 && p < 5)
				{
					offx = from.Location.X;
					offy = from.Location.Y-1;
					offz = from.Location.Z+height+2;
					height += 2;
				}
				else if(p >= 5 && p < 9)
				{
					offx = from.Location.X;
					offy = from.Location.Y-1;
					offz = from.Location.Z+height;
				}
				else
				{
					offx = from.Location.X+1;
					offy = from.Location.Y-1;
					offz = from.Location.Z+height;
				}
				Point3D loc = new Point3D(offx, offy, offz);
				librarybook.MoveToWorld(loc, from.Map);
				if( height == 0 )
					height = 8;
				height -= 2;
			}
		}

		public RuneLibraryTowns( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
