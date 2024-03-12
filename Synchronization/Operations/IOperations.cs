namespace Synchronization.Operations
{
    public interface IOperations
	{
        OperationImpl Copy();

        OperationImpl Delete();

        OperationImpl Update();
	}
}

