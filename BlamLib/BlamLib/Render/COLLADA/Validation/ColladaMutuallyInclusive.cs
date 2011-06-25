﻿/*
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
using System.Collections.Generic;

namespace BlamLib.Render.COLLADA.Validation
{
	/// <summary>
	/// Validation test that checks whether a list of elements that must exist together, do exist together
	/// </summary>
	public class ColladaMutuallyInclusive : ColladaValidationTest
	{
		private List<ColladaObject> _testFields;

		/// <summary>
		/// Validation test constructor
		/// </summary>
		/// <param name="valid_parent">The parent collada element type to run this test for. Set to "All" to run regardless of parent type</param>
		/// <param name="fields">A list of fields to run the test on</param>
		public ColladaMutuallyInclusive(Enums.ColladaElementType valid_parent, List<ColladaObject> fields)
			: base(valid_parent)
		{
			_testFields = fields;
		}

		/// <summary>
		/// Performs the validation test
		/// </summary>
		/// <returns></returns>
		protected override ColladaValidationException ValidateImpl()
		{
			ColladaValidationException exception = null;

			// determine whether one or more fields exists without the others
			bool any_present = false;
			bool all_present = true;
			foreach (var field in _testFields)
			{
				all_present &= (field.GetValue() != null);
				any_present |= (field.GetValue() != null);
			}

			// if an element is missing, when at least one is present, return an exception
			if (any_present && !all_present)
				exception = new ColladaValidationException(
					String.Format(exceptionFormat, "MutuallyInclusive", "N//A", "one or more elements missing that are mutually inclusive"));
			return exception;
		}
	}
}
