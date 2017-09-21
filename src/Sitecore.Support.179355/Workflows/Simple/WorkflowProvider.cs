namespace Sitecore.Support.Workflows.Simple
{
    using Sitecore.Data.Items;
    using Sitecore.Workflows;

    public class WorkflowProvider : Sitecore.Workflows.Simple.WorkflowProvider
    {
        public WorkflowProvider(string databaseName, HistoryStore historyStore) : base(databaseName, historyStore)
        {
        }

        public override IWorkflow[] GetWorkflows()
        {
            Item item = this.Database.Items[ItemIDs.WorkflowRoot];
            if (item == null)
            {
                return new IWorkflow[0];
            }
            #region Modified code
            // Item[] itemArray = item.Children.ToArray();
            Item[] itemArray = item.Axes.SelectItems("descendant::*[@@templateid='" + Sitecore.TemplateIDs.Workflow.ToString() + "']");

            if (itemArray != null)
            {
                IWorkflow[] workflowArray = new IWorkflow[itemArray.Length];
                for (int i = 0; i < itemArray.Length; i++)
                {
                    workflowArray[i] = this.InstantiateWorkflow(itemArray[i].ID.ToString(), this);
                }
                return workflowArray;
            }
            return new IWorkflow[0];     
            #endregion
        }
    }
}