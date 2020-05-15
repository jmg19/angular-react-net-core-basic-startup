using IntimateDesires.Business.Events;

namespace IntimateDesires.Application.Commands
{
    public abstract class AbstractComandHandler<T>
    {
        protected IBusinessEventsFactory businessEventsFactory;

        public AbstractComandHandler()
        {
            businessEventsFactory = new BusinessEventsFactory();
        }

        public AbstractComandHandler(IBusinessEventsFactory businessEventsFactory)
        {
            this.businessEventsFactory = businessEventsFactory;
        }

        public abstract void Handle(T command);
    }
}