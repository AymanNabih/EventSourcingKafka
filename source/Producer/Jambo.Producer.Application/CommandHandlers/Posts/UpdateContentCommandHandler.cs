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
    public class UpdateContentCommandHandler : AsyncRequestHandler<UpdatePostContentCommand>
    {
        private readonly IPublisher bus;
        private readonly IPostReadOnlyRepository postReadOnlyRepository;

        public UpdateContentCommandHandler(
            IPublisher bus,
            IPostReadOnlyRepository postReadOnlyRepository)
        {
            this.bus = bus ??
                throw new ArgumentNullException(nameof(bus));
            this.postReadOnlyRepository = postReadOnlyRepository ??
                throw new ArgumentNullException(nameof(postReadOnlyRepository));
        }

        protected override async Task Handle(UpdatePostContentCommand message, CancellationToken cancellationToken)
        {
            Post post = await postReadOnlyRepository.GetPost(message.Id);
            post.UpdateContent(Title.Create(message.Title), Content.Create(message.Content));

            await bus.Publish(post.GetEvents(), message.Header);
        }
    }
}

