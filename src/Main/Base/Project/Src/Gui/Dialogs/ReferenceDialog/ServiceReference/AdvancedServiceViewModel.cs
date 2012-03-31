﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;

using ICSharpCode.Core.Presentation;
using ICSharpCode.SharpDevelop.Widgets;

namespace ICSharpCode.SharpDevelop.Gui.Dialogs.ReferenceDialog.ServiceReference
{
	public enum Modifiers
	{
		//[Description("${res:Dialog.ProjectOptions.RunPostBuildEvent.Always}")]
		Public,
		//[Description("${res:Dialog.ProjectOptions.RunPostBuildEvent.OnOutputUpdated}")]
		Internal
	}
	
	public enum CollectionTypes
	{
		[Description("System.Array")]
		Array,
		[Description("System.Collections.ArrayList")]
		ArrayList,
		[Description("System.Collections.Generic.LinkedList")]
		LinkedList,
		[Description("System.Collections.Generic.List")]
		List,
		[Description("System.Collections.ObjectModel.Collection")]
		Collection,
		[Description("System.Collections.ObjectModel.ObservableCollection")]
		ObservableCollection,
		[Description("System.ComponentModel.BindingList")]
		BindingList
	}
	
	public enum DictionaryCollectionTypes
	{
		[Description("System.Collections.Generic.Dictionary")]
		Dictionary,
		[Description("System.Collections.Generic.SortedList")]
		SortedList,
		[Description("System.Collections.Generic.SortedDictionary")]
		SortedDictionary,
		[Description("System.Collections.Hashtable")]
		HashTable,
		[Description("System.Collections.ObjectModel.KeyedCollection")]
		KeyedCollection,
		[Description("System.Collections.SortedList")]
		SortedList_2,
		[Description("System.Collections.Specialized.HybridDictionary")]
		HybridDictionary,
		[Description("System.Collections.Specialized.ListDictionary")]
		ListDictionary,
		[Description("System.Collections.Specialized.OrderedDictionary")]
		OrderedDictionary
	}	
	
	internal class AdvancedServiceViewModel : ViewModelBase
	{
		string compatibilityText = "Add a Web Reference instead of a Service Reference. ";
		string c_2 = "This will generate code based on .NET Framework 2.0 Web Services technology.";
		string accesslevel = "Access level for generated classes:";
		ServiceReferenceGeneratorOptions options;
		
		public AdvancedServiceViewModel(ServiceReferenceGeneratorOptions options)
		{
			this.options = options;
			UpdateSettingsFromOptions();
			Title = "Service Reference Settings";
			UseReferencedAssemblies = true;
			BitmapSource image = PresentationResourceService.GetBitmapSource("Icons.16x16.Reference");
			AssembliesToReference = new ObservableCollection<CheckableImageAndDescription>();
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "Microsoft.CSharp"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "mscorlib"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Core"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Data"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Data.DataSetExtensions"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Runtime.Serialization"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.ServiceModel"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Xml"));
			AssembliesToReference.Add(new CheckableImageAndDescription(image, "System.Xml.Linq"));
		}
		
		public ServiceReferenceGeneratorOptions Options {
			get { return options; }
		}
		
		void UpdateSettingsFromOptions()
		{
			UpdateSelectedModifier();
			UpdateReferencedTypes();
		}
		
		void UpdateReferencedTypes()
		{
			if (options.UseTypesInProjectReferences) {
				this.ReuseTypes = true;
				this.UseReferencedAssemblies = true;
			} else {
				this.ReuseTypes = false;
				this.UseReferencedAssemblies = false;
			}
		}
		
		void UpdateSelectedModifier()
		{
			if (options.GenerateInternalClasses) {
				SelectedModifier = Modifiers.Internal;
			} else {
				SelectedModifier = Modifiers.Public;
			}
		}
		
		public string Title { get; set; }
		
		public string AccessLevel {
			get { return accesslevel; }
		}
		
		Modifiers selectedModifier;
		
		public Modifiers SelectedModifier {
			get { return selectedModifier; }
			set {
				selectedModifier = value;
				base.RaisePropertyChanged(() => SelectedModifier);
			}
		}
		
		public bool GenerateAsyncOperations {
			get { return options.GenerateAsyncOperations; }
			set {
				options.GenerateAsyncOperations = value;
				base.RaisePropertyChanged(() => GenerateAsyncOperations);
			}
		}
		
		public bool GenerateMessageContract {
			get { return options.GenerateMessageContract; }
			set {
				options.GenerateMessageContract = value;
				base.RaisePropertyChanged(() => GenerateMessageContract);
			}
		}
		
		public CollectionTypes CollectionType {
			get { return options.ArrayCollectionType; }
			set {
				options.ArrayCollectionType = value;
				base.RaisePropertyChanged(() => CollectionType);
			}
		}
		
		public DictionaryCollectionTypes DictionaryCollectionType {
			get { return options.DictionaryCollectionType; }
			set {
				options.DictionaryCollectionType = value;
				base.RaisePropertyChanged(() => DictionaryCollectionType);
			}
		}
		
		bool useReferencedAssemblies;
		
		public bool UseReferencedAssemblies {
			get { return useReferencedAssemblies; }
			set { 
				useReferencedAssemblies = value;
				ReuseTypes = useReferencedAssemblies;
				base.RaisePropertyChanged(() => UseReferencedAssemblies);
			}
		}
		
		public bool ReuseTypes {
			get { return options.UseTypesInProjectReferences; }
			set {
				options.UseTypesInProjectReferences = value;
				base.RaisePropertyChanged(() => ReuseTypes);
			}
		}
		
		bool reuseReferencedTypes;
		
		public bool ReuseReferencedTypes {
			get { return reuseReferencedTypes; }
			set { 
				reuseReferencedTypes = value;
				ListViewEnable = value;
				base.RaisePropertyChanged(() => ReuseReferencedTypes);
			}
		}
		
		bool listViewEnable;
		
		public bool ListViewEnable {
			get { return listViewEnable; }
			set {
				listViewEnable = value;
				base.RaisePropertyChanged(() => ListViewEnable);
			}
		}
		
		public ObservableCollection <CheckableImageAndDescription> AssembliesToReference { get; private set; }
		
		public string CompatibilityText 
		{
			get { return compatibilityText + c_2; }
		}	
	}
}
