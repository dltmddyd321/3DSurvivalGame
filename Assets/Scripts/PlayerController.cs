u                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             // 지면 체크
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        theCrosshair.JumpAnimation(!isGround);
    }

    private void TryJump() // 점프 시도
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround && theStatusSP.GetCurrentSP() > 0)
        {
            Jump();
        }
    }

    private void Jump() // 점프 동작
    {
        // 앉은 상태에서 점프 시 점프 해제
        if (isCrouch)
            Crouch();
        theStatusSP.DecreaseStamina(300);
        myRigid.velocity = transform.up * jumpForce;
    }

    void TryRun() // 달리기 시도
    {
        if (Input.GetKey(KeyCode.LeftShift) && theStatusSP.GetCurrentSP() > 0)
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || theStatusSP.GetCurrentSP() <= 0)
        {
            RunningCancel();
        }
    }

    private void Running() // 달리기 실행
    {
        // 앉은 상태에서 점프 시 점프 해제
        if (isCrouch)
            Crouch();
        theGunController.CancelFineSight();
        isRun = true;
        theCrosshair.RunningAnimation(isRun);
        isshoot = false;
        theStatusSP.DecreaseStamina(10);
        applySpeed = runSpeed;
    }

    private void RunningCancel() // 달리기 취소
    {
        isRun = false;
        theCrosshair.RunningAnimation(isRun);
        isshoot = true;
        applySpeed = walkSpeed;
    }

    private void Move() // 움직임 실행
    {

        float _moveDirX = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        float _moveDirZ = manager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck() // 움직임 체크
    {
        if (!isRun && !isCrouch && isGround)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else
                isWalk = false;

            theCrosshair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }

    }

    private void CharacterRotation() //좌우 캐릭터 회전
    {

        float _yRotation = manager.isAction ? 0 : Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation() // 상하 카메라 회전
    {

        float _xRotation = manager.isAction ? 0 : Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Coin"))
        {
            PlaySE(gainCoin);
            coinCount++;
            count.text = "Coin x " + coinCount.ToString();
        }
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }


}
