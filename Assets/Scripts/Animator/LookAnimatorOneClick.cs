using System.Collections.Generic;
using FIMSpace.FLook;
using UnityEngine;

public class LookAnimatorOneClick : MonoBehaviour
{
    [SerializeField] private string boneName = "CC_Base_Waist";
    [SerializeField] private IntegerVariable _integerVariable;



#if UNITY_EDITOR
    public void BoneCorrection()
    {
        AreaPrefab _areaPrefab = GetComponent<AreaPrefab>();
        
        foreach (Transform npc in _areaPrefab.characterContainer)
        {
            var found = false;

            NpcInteraction npcInteraction = npc.GetComponentInChildren<NpcInteraction>();
            if (npcInteraction._integerVariable == null)
            {
                npcInteraction._integerVariable = _integerVariable;
            }
            
            CharacterAdjustment characterAdjustment = npc.GetComponentInChildren<CharacterAdjustment>();
            if (characterAdjustment.characterPivot == null)
            {
                characterAdjustment.characterPivot = characterAdjustment.transform.GetChild(0).GetChild(0);
            }
            if (characterAdjustment.uiPivot == null)
            {
                characterAdjustment.uiPivot = characterAdjustment.characterPivot.transform.GetChild(0);
            }

            FLookAnimator _fLookAnimator = npc.GetComponentInChildren<FLookAnimator>();

            if (_fLookAnimator == null)
            {
                Debug.Log("No Look Animator Found");
                Animator _animator = npc.GetComponentInChildren<Animator>();

                _fLookAnimator = _animator.gameObject.AddComponent<FLookAnimator>();
            }

            _fLookAnimator.LeadBone = null;
            FindHeadBone(_fLookAnimator);
            
            _fLookAnimator.LookBones.Clear();
            _fLookAnimator.BackBonesCount = 3;
            _fLookAnimator.UpdateForCustomInspector();

            //Debug.LogWarning("Compensation Bones: " + _fLookAnimator.CompensationBones.Count);
            _fLookAnimator.CompensationBones?.Clear();
            _fLookAnimator.CompensationBones = new List<FLookAnimator.CompensationBone>();
            FindCompensationBones(_fLookAnimator);

            //_fLookAnimator.CompensationBones
            
            // change follow mode
            _fLookAnimator.FollowMode = FLookAnimator.EFFollowMode.ToFollowSpaceOffset;
            
            // animation behaviour
            _fLookAnimator.MaximumDistance = 2.5f;
            _fLookAnimator.MaxOutDistanceFactor = 0.5f;
            
            // correction
            _fLookAnimator.FixingPreset = FLookAnimator.EFAxisFixOrder.Parental;

            // 1st hierarchy
            foreach (Transform child1 in _fLookAnimator.transform)
            {
                // SET OPTIMIZE WITH MESH
                SkinnedMeshRenderer skinnedMeshRenderer = child1.GetComponent<SkinnedMeshRenderer>();
                if (skinnedMeshRenderer != null && skinnedMeshRenderer.rootBone.name == boneName)
                {
                    _fLookAnimator.OptimizeWithMesh = skinnedMeshRenderer;
                }

                if (child1.name == boneName)
                {
                    AssignBone(_fLookAnimator, child1);
                    Debug.LogWarning("Bone Correction, " + npc.name + "'s bone correction assigned: " + child1.name);
                    break;
                }

                if (child1.childCount == 0) continue;

                // 2nd hierarchy
                foreach (Transform child2 in child1)
                {
                    if (child2.name == boneName)
                    {
                        AssignBone(_fLookAnimator, child2);
                        Debug.LogWarning("Bone Correction, " + npc.name + "'s bone correction assigned: " + child2.name);
                        found = true;
                        break;
                    }
                    
                    if (child1.childCount == 0) continue;
                    
                    // 3rd hierarchy
                    foreach (Transform child3 in child2)
                    {
                        if (child3.name == boneName)
                        {
                            AssignBone(_fLookAnimator, child3);
                            Debug.LogWarning("Bone Correction, " + npc.name + "'s bone correction assigned: " + child3.name);
                            found = true;
                            break;
                        }
                    
                        if (child1.childCount == 0) continue;
                        
                        // 4th hierarchy
                        foreach (Transform child4 in child3)
                        {
                            if (child4.name != boneName) continue;
                            
                            AssignBone(_fLookAnimator, child4);
                            Debug.LogWarning("Bone Correction, " + npc.name + "'s bone correction assigned: " + child4.name);
                            found = true;
                            break;
                        }
                        if (found)
                        {
                            break;
                        }
                    }
                    if (found)
                    {
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

            // Clamp Angle Horizontal
            _fLookAnimator.XRotationLimits = new Vector2(-105, 105);
            
            // Clamp Angle Vertical
            _fLookAnimator.YRotationLimits.x = -10;
            
            // Capsule Collider
            Rigidbody _rigidbody = _fLookAnimator.GetComponent<Rigidbody>();
            if (_rigidbody == null) _rigidbody = _fLookAnimator.gameObject.AddComponent<Rigidbody>();

            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            
            // Capsule Collider
            CapsuleCollider _capsuleCollider = _fLookAnimator.GetComponent<CapsuleCollider>();
            if (_capsuleCollider == null) _capsuleCollider = _fLookAnimator.gameObject.AddComponent<CapsuleCollider>();
            
            _capsuleCollider.center = new Vector3(0, 0.95f, 0);
            _capsuleCollider.radius = 0.35f;
            _capsuleCollider.height = 1.9f;

            // Sphere Collider
            SphereCollider _sphereCollider = _fLookAnimator.GetComponent<SphereCollider>();
            if (_sphereCollider == null) _sphereCollider = _fLookAnimator.gameObject.AddComponent<SphereCollider>();

            _sphereCollider.radius = 3;
            
            // Animation Idle Checker
            IdleAnimationInRangeChecker _idleAnimationInRangeChecker = _fLookAnimator.GetComponent<IdleAnimationInRangeChecker>();
            if (_idleAnimationInRangeChecker == null) _fLookAnimator.gameObject.AddComponent<IdleAnimationInRangeChecker>();
        }

        void AssignBone(FLookAnimator lookAnimator, Transform boneWaist)
        {
            lookAnimator.ParentalReferenceBone = boneWaist;
            lookAnimator.ConstantParentalAxisUpdate = true;
        }
    }
#endif
    

    public void Finished()
    {
        DestroyImmediate(this);
    }
    
    
    //private FLookAnimator Get { get { if (_get == null) _get = target as FLookAnimator; return _get; } }
    //private FLookAnimator _get;
    
    private void FindHeadBone(FLookAnimator headLook)
        {
            // First let's check if it's humanoid character, then we can get head bone transform from it
            Transform root = headLook.transform;
            if (headLook.BaseTransform) root = headLook.BaseTransform;

            Animator animator = root.GetComponentInChildren<Animator>();
            Transform animatorHeadBone = null;
            if (animator)
            {
                if (animator.isHuman)
                    animatorHeadBone = animator.GetBoneTransform(HumanBodyBones.Head);
            }

            List<SkinnedMeshRenderer> sMeshs = new List<SkinnedMeshRenderer>();// = root.GetComponentInChildren<SkinnedMeshRenderer>();

            foreach (var tr in root.GetComponentsInChildren<Transform>())
            {
                if (tr == null) continue;
                SkinnedMeshRenderer sMesh = tr.GetComponent<SkinnedMeshRenderer>();
                if (sMesh) sMeshs.Add(sMesh);
            }

            Transform leadBone = null;
            Transform probablyWrongTransform = null;

            for (int s = 0; s < sMeshs.Count; s++)
            {
                Transform t;

                for (int i = 0; i < sMeshs[s].bones.Length; i++)
                {
                    t = sMeshs[s].bones[i];
                    if (t.name.ToLower().Contains("head"))
                    {
                        if (t.parent == root) continue; // If it's just mesh object from first depths

                        leadBone = t;
                        break;
                    }
                }

                if (!leadBone)
                    for (int i = 0; i < sMeshs[s].bones.Length; i++)
                    {
                        t = sMeshs[s].bones[i];
                        if (t.name.ToLower().Contains("neck"))
                        {
                            leadBone = t;
                            break;
                        }
                    }
            }


            foreach (Transform t in root.GetComponentsInChildren<Transform>())
            {
                if (t.name.ToLower().Contains("head"))
                {
                    if (t.GetComponent<SkinnedMeshRenderer>())
                    {
                        if (t.parent == root) continue; // If it's just mesh object from first depths
                        probablyWrongTransform = t;
                        continue;
                    }

                    leadBone = t;
                    break;
                }
            }

            if (!leadBone)
                foreach (Transform t in root.GetComponentsInChildren<Transform>())
                {
                    if (t.name.ToLower().Contains("neck"))
                    {
                        leadBone = t;
                        break;
                    }
                }

            if (leadBone == null && animatorHeadBone != null)
                leadBone = animatorHeadBone;
            else
            if (leadBone != null && animatorHeadBone != null)
            {
                if (animatorHeadBone.name.ToLower().Contains("head")) leadBone = animatorHeadBone;
                else
                    if (!leadBone.name.ToLower().Contains("head")) leadBone = animatorHeadBone;
            }

            if (leadBone)
            {
                headLook.LeadBone = leadBone;
            }
            else
            {
                if (probablyWrongTransform)
                {
                    //headLook.LeadBone = probablyWrongTransform;
                    Debug.LogWarning("[LOOK ANIMATOR] Found " + probablyWrongTransform + " but it's probably wrong transform");
                }
                else
                {
                    Debug.LogWarning("[LOOK ANIMATOR] Couldn't find any fitting bone");
                }
            }
        }

    private void FindCompensationBones(FLookAnimator headLook)
    {
        int compensationBonesCount = 0;
        // First let's check if it's humanoid character, then we can get head bone transform from it
            Transform root = headLook.transform;
            if (headLook.BaseTransform) root = headLook.BaseTransform;

            Animator animator = root.GetComponentInChildren<Animator>();

            List<Transform> compensationBones = new List<Transform>();

            Transform headBone = headLook.LeadBone;

            if (animator)
            {
                if (animator.isHuman)
                {
                    Transform b = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
                    if (b) compensationBones.Add(b);

                    b = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
                    if (b) compensationBones.Add(b);

                    b = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
                    if (b) compensationBones.Add(b);

                    b = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
                    if (b) compensationBones.Add(b);

                    if (!headBone) animator.GetBoneTransform(HumanBodyBones.Head);
                }
                else
                {
                    if (animator)
                    {
                        foreach (Transform t in animator.transform.GetComponentsInChildren<Transform>())
                        {
                            if (t.name.ToLower().Contains("clav"))
                            {
                                if (!compensationBones.Contains(t)) compensationBones.Add(t);
                            }
                            else
                            if (t.name.ToLower().Contains("shoulder"))
                            {
                                if (!compensationBones.Contains(t)) compensationBones.Add(t);
                            }
                            else
                            if (t.name.ToLower().Contains("uppera"))
                            {
                                if (!compensationBones.Contains(t)) compensationBones.Add(t);
                            }
                        }
                    }
                }
            }

            //headLook.CompensationBones = compensationBones;

            Debug.LogWarning("Compensation Bones Count: " + compensationBones.Count);
            
            if (compensationBones.Count != 0)
            {
                for (int i = 0; i < compensationBones.Count; i++)
                {
                    // Checking if this bone is not already in compensation bones list
                    bool already = false;
                    for (int c = 0; c < headLook.CompensationBones.Count; c++)
                    {
                        if (compensationBones[i] == headLook.CompensationBones[c].Transform)
                        {
                            already = true;
                            break;
                        }
                    }

                    if (already) continue;

                    // Fill nulls if available
                    bool filled = false;
                    for (int c = 0; c < headLook.CompensationBones.Count; c++)
                    {
                        if (headLook.CompensationBones[c].Transform == null)
                        {
                            headLook.CompensationBones[c] = new FLookAnimator.CompensationBone(compensationBones[i]);
                            filled = true;
                            
                            //serializedObject.Update();
                            //serializedObject.ApplyModifiedProperties();
                            break;
                        }
                    }

                    if (!filled)
                        headLook.CompensationBones.Add(new FLookAnimator.CompensationBone(compensationBones[i]));
                }

                for (int c = headLook.CompensationBones.Count - 1; c >= 0; c--)
                {
                    if (headLook.CompensationBones[c].Transform == null) headLook.CompensationBones.RemoveAt(c);
                }

                compensationBonesCount = headLook.CompensationBones.Count;
            }                    
    }    
}
