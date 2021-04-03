// <copyright file="GlobalSurpressions.cs" company="Kwetter">
//     Copyright Kwetter. All rights reserved.
// </copyright>
// <author>Dirk Heijnen</author>

// For contracts where a single requesthandler can only have a single request, or interfaces which have only one implementation for DI.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:File may only contain a single type", Justification = "Handler are matched with request for readablity.", Scope = "module")]

// Even if copyright header is correct this warning occurs, therefor disabled this.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1636:TheFileHeaderCopyrightTextShouldMatchTheCopyrightTextFromSettings", Justification = "Reviewed.", Scope = "module")]

// Code is more readible if this syntax can be used on a single line: (public class A : B)
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1127:GenericTypesMustBeOnOwnLine", Justification = "Reviewed.", Scope = "module")]

// Code is more readible if this syntax can be used on a single line: (public DbContext(options) : base(options))
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1128:ConstructorInitializersMustBeOnOwnLine", Justification = "Reviewed.", Scope = "module")]

// Warning cannot be resolved without creating another warning and code obfuscation, this is only for using tuples.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1414:TupleTypesInSignatureShouldHaveElementNames", Justification = "Reviewed.", Scope = "module")]

// Constructor parameters must be on a single line, this is not always possible with long classnames.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1116:ParametersShouldBeginOnOwnLine", Justification = "Reviewed.", Scope = "module")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1117:ConstructorParameterMustBeOnSingleLine", Justification = "Reviewed.", Scope = "module")]

// When swagger is used to generate documentation via XML comments it will show this warning in packages which don't create a documentation file.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA0001:XmlCommentAnalysisDisabled", Justification = "Reviewed.", Scope = "module")]

// Single line commments must be preceeded by blank line, this is a warning that happens in the file header and cannot be resolved.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1515:SingleLineCommentsShouldBePreceededByBlankLine", Justification = "Reviewed.", Scope = "module")]

// Documenting enum types just makes it harder to read.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:EnumerationItemsMustBeDocumented", Justification = "Reviewed.", Scope = "module")]

// A constructor must not follow a property.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Reviewed.", Scope = "module")]
