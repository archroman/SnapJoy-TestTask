using System.Collections;
using _ProjectFiles.Player.Scripts;
using UnityEngine;

namespace _ProjectFiles.Items.Scripts
{
    public class NoteItem : Item
    {
        [SerializeField] private Transform _rightPage;
        [SerializeField] private float _openAngle = -120f;
        [SerializeField] private float _speed = 200f;

        private bool _opened;

        public override void OnInteractStart(PlayerInteractor player)
        {
            base.OnInteractStart(player);

            if (!_opened)
                StartCoroutine(OpenAnimation());
        }

        private IEnumerator OpenAnimation()
        {
            _opened = true;

            float current = 0f;

            while (current > _openAngle)
            {
                current -= _speed * Time.deltaTime;
                _rightPage.localRotation = Quaternion.Euler(0, current, 0);
                yield return null;
            }

            _rightPage.localRotation = Quaternion.Euler(0, _openAngle, 0);
        }
    }
}