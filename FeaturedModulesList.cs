using System;
using System.Collections.Generic;
using UnityEngine;
using ZE.FeaturedScopes;

#if !NO_TRI_INSPECTOR
using TriInspector;
#endif

// note: sometimes metadata brokes, check modules asset when after hard branch switch

[CreateAssetMenu(
        fileName = nameof(FeaturesModulesList),
        menuName = "Bootstrap/Features Modules List")]
public sealed class FeaturesModulesList : ScriptableObject
{
    [SerializeReference]
#if !NO_TRI_INSPECTOR
    [ValidateInput(nameof(ValidateNoDuplicateReferences))]
#endif
    private List<IFeatureModule> _modules = new();
    //---
    public IReadOnlyList<IFeatureModule> Modules => _modules;

#if !NO_TRI_INSPECTOR
    private TriValidationResult ValidateNoDuplicateReferences()
    {
        var uniqueTypes = new HashSet<Type>();

        foreach (var item in _modules)
        {
            if (item == null) continue;
            var type = item.GetType();

            if (uniqueTypes.Contains(type))
            {
                return TriValidationResult.Error($"Duplicated {type.Name}");
            }
            
            uniqueTypes.Add(type);
        }

        return TriValidationResult.Valid;
    }
#endif
}

