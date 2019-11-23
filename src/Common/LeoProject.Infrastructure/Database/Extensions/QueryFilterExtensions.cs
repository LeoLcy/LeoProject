﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Remotion.Linq.Parsing.ExpressionVisitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace LeoProject.Infrastructure.Database.Extensions
{
    public static class QueryFilterExtensions
    {
        public static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder,
            Expression<Func<T, bool>> expression)
        {
            var parameterType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var internalEntityTypeBuilder = entityTypeBuilder.GetInternalEntityTypeBuilder();
            if (internalEntityTypeBuilder.Metadata.QueryFilter != null)
            {
                var currentQueryFilter = internalEntityTypeBuilder.Metadata.QueryFilter;
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }

        internal static InternalEntityTypeBuilder GetInternalEntityTypeBuilder(this EntityTypeBuilder entityTypeBuilder)
        {
            var internalEntityTypeBuilder = typeof(EntityTypeBuilder)
                .GetProperty("Builder", BindingFlags.NonPublic | BindingFlags.Instance)?
                .GetValue(entityTypeBuilder) as InternalEntityTypeBuilder;

            return internalEntityTypeBuilder;
        }
    }
}
