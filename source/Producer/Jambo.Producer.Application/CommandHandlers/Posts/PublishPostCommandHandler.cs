using Jambo.Producer.Application.Commands;
using Jambo.Producer.Application.Commands.Blogs;
using Jambo.Producer.Application.Commands.Posts;
using Jambo.Domain.Model.Blogs;
using Jambo.Domain.Model.Posts;
using MediatR;
using System;
using System.Threading.Tasks;
using Jambo.Domain.ServiceBus;
using System.Threading;

namespace Jambo.Producer.Application.CommandHandlers.Posts
{
    public class PublishPostCommandHandler : AsyncRequestHandler<PublishPostCommand>
    {
        private readonly IPublisher bus;
        private readonly IPostReadOnlyRepository postReadOnlyRepository;

        public PublishPostCommandHandler(
            IPublisher bus,
            IPostReadOnlyRepository postReadOnlyRepository)
        {
            this.bus = bus ??
                throw new ArgumentNullException(nameof(bus));
            this.postReadOnlyRepository = postReadOnlyRepository ??
                throw new ArgumentNullException(nameof(postReadOnlyRepository));
        }

        protected override async Task Handle(PublishPostCommand message, CancellationToken cancellationToken)
        {
            Post post = await postReadOnlyRepository.GetPost(message.Id);
            post.Publish();

            await bus.Publish(post.GetEvents(), message.Header);
        }
    }
}
