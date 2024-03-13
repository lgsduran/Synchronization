using Synchronization.Extensions;

namespace Synchronization.Operations
{
    public class OperationImpl : IOperations
	{
        private DirectoryInfo _source = null!;
        private DirectoryInfo _target = null!;

        public OperationImpl InitializeOperation(string source, string target)
		{            
            _source = new DirectoryInfo(source);
            _target = new DirectoryInfo(target);
            
            if (string.Equals(_source.FullName, _target.FullName, StringComparison.OrdinalIgnoreCase))
                throw new DirectoryNotFoundException("Directory not found!");            

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

