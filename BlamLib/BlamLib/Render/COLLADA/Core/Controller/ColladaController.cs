/*
    BlamLib: .NET SDK for the Blam Engine

    Copyright (C) 2005-2010  Kornner Studios (http://kornner.com)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using BlamLib.Render.COLLADA.Validation;

namespace BlamLib.Render.COLLADA.Core
{
	[SerializableAttribute()]
	[XmlTypeAttribute(AnonymousType = true)]
	public partial class ColladaController : ColladaElement
	{
		[XmlIgnore]
		public new static readonly string ElementIDFormat = "controller-{0}";

		#region Fields
		ColladaObjectAttribute<string> _id;
		ColladaObjectAttribute<string> _name;

		ColladaObjectElement<ColladaAsset> _asset;
		ColladaObjectElement<ColladaSkin> _skin;
		ColladaObjectElement<ColladaMorph> _morph;
		ColladaObjectElementList<ColladaExtra> _extra;
		#endregion

		#region Attributes
		[XmlAttribute("id"), DefaultValue("")]
		public string ID
		{ get { return _id.Value; } set { _id.Value = value; } }

		[XmlAttribute("name"), DefaultValue("")]
		public string Name
		{ get { return _name.Value; } set { _name.Value = value; } }
		#endregion

		#region Children
		[XmlElement("asset")]
		public ColladaAsset Asset
		{ get { return _asset.Value; } set { _asset.Value = value; } }

		[XmlElement("skin")]
		public ColladaSkin Skin
		{ get { return _skin.Value; } set { _skin.Value = value; } }

		[XmlElement("morph")]
		public ColladaMorph Morph
		{ get { return _morph.Value; } set { _morph.Value = value; } }

		[XmlElement("extra")]
		public List<ColladaExtra> Extra
		{ get { return _extra.Value; } set { _extra.Value = value; } }
		#endregion

		public ColladaController() : base(Enums.ColladaElementType.Core_Controller)
		{
			Fields.Add(_id = new ColladaObjectAttribute<string>(""));
			Fields.Add(_name = new ColladaObjectAttribute<string>(""));
			Fields.Add(_asset = new ColladaObjectElement<ColladaAsset>());
			Fields.Add(_skin = new ColladaObjectElement<ColladaSkin>());
			Fields.Add(_morph = new ColladaObjectElement<ColladaMorph>());
			Fields.Add(_extra = new ColladaObjectElementList<ColladaExtra>());

			List<ColladaObject> mutually_exclusive = new List<ColladaObject>();
			mutually_exclusive.Add(_skin);
			mutually_exclusive.Add(_morph);

			ValidationTests.Add(new ColladaMutuallyExclusive(Enums.ColladaElementType.All, mutually_exclusive));
			ValidationTests.Add(new ColladaOneRequired(Enums.ColladaElementType.All, mutually_exclusive));
		}
	}
}