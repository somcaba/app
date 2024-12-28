namespace Yacaba.Core.Exceptions {
    public class EntityNotFoundExeption : Exception {

        public String Entity { get; }
        public Object Id { get; }

        public EntityNotFoundExeption(String entity, Object id) {
            Entity = entity;
            Id = id;
        }

    }
}
