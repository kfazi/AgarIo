// ReSharper disable once CheckNamespace
namespace Caliburn.Micro.Validation
{
    using System;
    using System.Linq.Expressions;

    public static class ExpressionExtensions
    {
        internal static string GetPropertyFullName(this Expression propertyExpression)
        {
            while (propertyExpression != null)
            {
                var memberExpression = propertyExpression as MemberExpression;
                if (memberExpression != null)
                {
                    return GetPropertyName(memberExpression);
                }

                var unaryExpression = propertyExpression as UnaryExpression;
                if (unaryExpression != null)
                {
                    return GetPropertyName(unaryExpression.Operand as MemberExpression);
                }

                var lambdaExpression = propertyExpression as LambdaExpression;
                if (lambdaExpression == null)
                {
                    break;
                }

                propertyExpression = lambdaExpression.Body;
            }

            throw new ApplicationException($"Expression: {propertyExpression} is not MemberExpression");
        }

        private static string GetPropertyName(MemberExpression me)
        {
            var propertyName = me.Member.Name;

            if (me.Expression.NodeType != ExpressionType.Parameter
                && me.Expression.NodeType != ExpressionType.TypeAs
                && me.Expression.NodeType != ExpressionType.Constant)
            {
                propertyName = GetPropertyName(me.Expression as MemberExpression) + "." + propertyName;
            }

            return propertyName;
        }
    }
}