using System.Collections.Generic;
using TeamCitySharper.DomainEntities;
using TeamCitySharper.Locators;

namespace TeamCitySharper.ActionTypes
{
    public interface IVcsRoots
    {
        List<VcsRoot> All();
        VcsRoot ById(string vcsRootId);
        VcsRoot AttachVcsRoot(BuildTypeLocator locator, VcsRoot vcsRoot);
        void DetachVcsRoot(BuildTypeLocator locator, string vcsRootId);
        void SetVcsRootField(VcsRoot vcsRoot, VcsRootField field, object value);
    }
}