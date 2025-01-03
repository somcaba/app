using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.OData.UriParser;

namespace Yacaba.Api.OData.FilterBinders {
    public class CustomFilterBinder : FilterBinder {

        //public override Expression BindSingleValueNode(Microsoft.OData.UriParser.SingleValueNode node, QueryBinderContext context) {
        //    switch (node.Kind) {
        //        case Microsoft.OData.UriParser.QueryNodeKind.SingleValuePropertyAccess:
        //            return CustomSingleValuePropertyAccess(node, context);
        //    }
        //    return base.BindSingleValueNode(node, context);
        //}

        public override Expression BindPropertyAccessQueryNode(Microsoft.OData.UriParser.SingleValuePropertyAccessNode propertyAccessNode, QueryBinderContext context) {
            if (propertyAccessNode.Property.Name == "id") {
                return Expression.Property(context.CurrentParameter, "id");
            }
            return base.BindPropertyAccessQueryNode(propertyAccessNode, context);
        }

        public override Expression BindBinaryOperatorNode(Microsoft.OData.UriParser.BinaryOperatorNode binaryOperatorNode, QueryBinderContext context) {

            if (
                binaryOperatorNode.Left.Kind == Microsoft.OData.UriParser.QueryNodeKind.SingleValuePropertyAccess &&
                binaryOperatorNode.Left is SingleValuePropertyAccessNode { Property.Name: "id" } &&
                binaryOperatorNode.Right.Kind == QueryNodeKind.Constant &&
                binaryOperatorNode.Right is ConstantNode constantNode && constantNode.Value is Int64 constantNodeValue
            ) {
                MemberExpression left = Expression.Property(context.CurrentParameter, "id");
                //Expression right = Expression.Constant(new OrganisationId(constantNodeValue));
                Expression right = Expression.Constant(Activator.CreateInstance(((PropertyInfo)left.Member).PropertyType, [constantNodeValue]));

                Type? expressionBinderHelperType = typeof(Microsoft.AspNetCore.OData.Query.Expressions.IFilterBinder).Assembly.GetTypes().SingleOrDefault(p => p.Name == "ExpressionBinderHelper");
                System.Reflection.MethodInfo? createBinaryExpressionMethod = expressionBinderHelperType?.GetMethod("CreateBinaryExpression", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                return (Expression)createBinaryExpressionMethod.Invoke(null, [binaryOperatorNode.OperatorKind, left, right, false, context.QuerySettings]);
            }

            return base.BindBinaryOperatorNode(binaryOperatorNode, context);
        }

        //private Expression CustomSingleValuePropertyAccess(Microsoft.OData.UriParser.SingleValueNode node, QueryBinderContext context) {
        //    return null;
        //}

    }
}
