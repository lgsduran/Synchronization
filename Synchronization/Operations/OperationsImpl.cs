using Synchronization.Extensions;

namespace Synchronization.Operations
{
    public class OperationImpl : IOperations
	{
        private DirectoryInfo _source = null!;
        private DirectoryInfo _target = null!;

        public OperationImpl InitializeOperation(DirectoryInfo source, DirectoryInfo target)
		{            
            if (string.Equals(source.FullName, target.FullName, StringComparison.OrdinalIgnoreCase))
                throw new DirectoryNotFoundException("Directory not found!");

            _source = source;
            _target = target;           

            return this;
        }

        public OperationImpl Copy()
        {
            _source.Copy(_target);
            return this;
        }

        public OperationImpl Delete()
        {
            _source.Delete(_target);
            return this;
        }

        public OperationImpl Update()
        {
            _source.Update(_target);
            return this;
        }
    }
}

