    using UniRx;
    using UnityEngine;

    public interface IInputEventProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
    }
