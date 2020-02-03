// -----------------------------------------------------------------------
//  <copyright file="ExampleJsInterop.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.AspNetCore.Components
{
    using System;
    using Extensions.Localization;
    using Extensions.Localization.Interfaces;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Components;

    public abstract class LocalizationComponentBase<T> : ComponentBase
            where T : class, new()
    {
        [NotNull]
        protected T LocalizationContext { get; private set; } = LocalizationDefinitionConvention.CreateLocalizationInstance<T>(c => "...");

        [Inject]
        [UsedImplicitly]
        [NotNull]
        ILocalizationCache Localization { get; set; }

        /// <inheritdoc />
        protected override void OnInitialized()
        {
            LocalizationContext = Localization.CreateContext<T>().Value;
        }
    }

    public interface IFooterLocalization
    {
        LocalizationValue AllRightsReserved { get; }
    }

    public class FooterComponentOptions
    {
        public string AuthorName { get; set; }

        public string CopyrigthText { get; set; } = $"&copy; {DateTime.Today.Year:D} {{%AllRightsReserved%}}.";
    }
}