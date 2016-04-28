﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Models.Core;

namespace Models.PMF.Functions
{
    /// <summary>
    /// Raises the value of the child to the power of the exponent specified
    /// </summary>
    [Serializable]
    [Description("Raises the value of the child to the power of the exponent specified")]
    [ViewName("UserInterface.Views.GridView")]
    [PresenterName("UserInterface.Presenters.PropertyPresenter")]
    public class PowerFunction : Model, IFunction
    {
        /// <summary>The exponent value that the subjuct is raised to</summary>
        [Link]
        IFunction Exponent = null;

        /// <summary>The number that is to be raised by the exponent</summary>
        [Link]
        IFunction Subject = null;

        /// <summary>Gets the value.</summary>
        /// <value>The value.</value>
        /// <exception cref="System.Exception">Power function must have only one argument</exception>
        public double Value
        {
            get
            {
                return Math.Pow(Subject.Value, Exponent.Value);
            }
        }
        /// <summary>Writes documentation for this function by adding to the list of documentation tags.</summary>
        /// <param name="tags">The list of tags to add to.</param>
        /// <param name="headingLevel">The level (e.g. H2) of the headings.</param>
        /// <param name="indent">The level of indentation 1, 2, 3 etc.</param>
        public override void Document(List<AutoDocumentation.ITag> tags, int headingLevel, int indent)
        {
            // add a heading.
            Name = this.Name;
            tags.Add(new AutoDocumentation.Heading(Name, headingLevel));

            tags.Add(new AutoDocumentation.Paragraph("<i>" + this.Name + "</i> returns Subject.Value raised to the power of " + Exponent.Value, indent));

            tags.Add(new AutoDocumentation.Paragraph("Where:", indent));
            
            // write children.
            foreach (IModel child in Apsim.Children(this, typeof(IModel)))
            {
                if (child.Name == "Subject")
                child.Document(tags, headingLevel + 1, indent + 1);
            }
        }


    }
}
