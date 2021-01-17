﻿//-----------------------------------------------------------------------
// <copyright file="AreaRegionMessageExtractor.cs" company="Thomas Stollenwerk (motmot80)">
// Copyright (c) Thomas Stollenwerk (motmot80). All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Abune.Server.Sharding
{
    using System.Globalization;
    using Abune.Server.Command;
    using Abune.Shared.Command.Contract;
    using Abune.Shared.Message.Area;
    using Abune.Shared.Message.Contract;
    using Akka.Cluster.Sharding;

    /// <summary>Message extractor for area shard regions.</summary>
    public sealed class AreaRegionMessageExtractor : HashCodeMessageExtractor
    {
        /// <summary>Initializes a new instance of the <see cref="AreaRegionMessageExtractor"/> class.</summary>
        /// <param name="numberOfShards">The number of shards.</param>
        public AreaRegionMessageExtractor(int numberOfShards)
            : base(numberOfShards)
        {
        }

        /// <summary>
        /// Builds the entity identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="areaId">The area identifier.</param>
        /// <returns>The entity id.</returns>
        public static string BuildEntityId(ulong sessionId, ulong areaId)
        {
            return $"{sessionId}-{areaId}";
        }

        /// <summary>Extracts the identifier of the entity.</summary>
        /// <param name="message">Message to process.</param>
        /// <returns>Entity identifier.</returns>
        public override string EntityId(object message)
        {
            if (message is ICanRouteToArea canRouteToArea)
            {
                return BuildEntityId(canRouteToArea.ToSessionId, canRouteToArea.ToAreaId);
            }

            return string.Empty;
        }

        /// <summary>Extracts the entity message.</summary>
        /// <param name="message">Message to process.</param>
        /// <returns>Message object.</returns>
        public override object EntityMessage(object message) => message;
    }
}
