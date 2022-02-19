using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automatonymous;

namespace PixelService.Command.Saga
{
	public class TestState : 
		SagaStateMachineInstance
	{
		public Guid CorrelationId { get; set; }

		public string CurrentState { get; set; }
	}

	public class TestStateMachine :
		MassTransitStateMachine<TestState>
	{
		public State Submitted { get; private set; }
		public State Accepted { get; private set; }

		public TestStateMachine()
		{
			Event(() => SubmitOrder, 
				x => x.CorrelateById(conntext => conntext.Message.CorrelationId));
			
			Initially(
				When(SubmitOrder)
					.Then(Initialize)
					.TransitionTo(Submitted));

			During(Submitted, 
				When(SubmitOrder)
					.TransitionTo(Accepted));
		}

		void Initialize(BehaviorContext<TestState, TestState> context)
		{
			context.Instance.CurrentState = context.Data.CurrentState;
			context.Instance.CorrelationId = context.Data.CorrelationId;
		}

		public Event<TestState> SubmitOrder { get; private set; }
	}


}
